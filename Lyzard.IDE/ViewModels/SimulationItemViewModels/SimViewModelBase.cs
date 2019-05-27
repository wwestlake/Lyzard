/* 
 * Lyzard Modeling and Simulation System
 * 
 * Copyright 2019 William W. Westlake Jr.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using Lyzard.CustomControls;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Controls;

namespace Lyzard.IDE.ViewModels.SimulationItemViewModels
{
    public abstract class SimViewModelBase : ViewModelBase
    {


        internal abstract void HandleConnectionAdded(Connector connector);
        internal abstract Delegate ConnectToOutput(Connection connection);
        internal abstract void OnDelete();

        internal abstract void OnDeleteConnection(Connection connection);


        protected void SetDelegate(Connector connector, Expression<Func<DoubleDelegate>> source, PropertyChangedEventHandler handler, Expression<Func<double>> variable)
        {
            var sourceExpr = (MemberExpression)source.Body;
            var soourceProp = (PropertyInfo)sourceExpr.Member;
            var sinkExpr = (MemberExpression)variable.Body;
            var sinkProp = (PropertyInfo)sinkExpr.Member;

            var Source = source.Compile();



            try
            {
                foreach (var connection in connector.Connections)
                {
                    if (connection.Source != null)
                    {
                        var vm = (connection.Source.ParentDesignerItem.Content as Control).DataContext as SimViewModelBase;
                        soourceProp.SetValue(this, vm.ConnectToOutput(connection) as DoubleDelegate);
                        vm.PropertyChanged += handler;
                        sinkProp.SetValue(this, Source());
                    }
                }
            }
            catch
            {
            }
        }

        protected void RemoveDelegate(Connector connector, Expression<Func<DoubleDelegate>> source, PropertyChangedEventHandler handler, Expression<Func<double>> variable)
        {
            var sourceExpr = (MemberExpression)source.Body;
            var soourceProp = (PropertyInfo)sourceExpr.Member;
            var sinkExpr = (MemberExpression)variable.Body;
            var sinkProp = (PropertyInfo)sinkExpr.Member;

            var Source = source.Compile();

            try
            {
                foreach (var connection in connector.Connections)
                {
                    if (connection.Source != null)
                    {
                        var vm = (connection.Source.ParentDesignerItem.Content as Control).DataContext as SimViewModelBase;
                        soourceProp.SetValue(this, null);
                        vm.PropertyChanged -= handler;
                    }
                }
            }
            catch
            {
            }

        }



    }
}
