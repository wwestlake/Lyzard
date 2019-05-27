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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lyzard.Interfaces
{

    /// <summary>
    /// Substitutes for the windows MessageBoxButton
    /// </summary>
    public enum MessageBoxButtons
    {
        OK,
        OKCancel,
        YesNo,
        YesNoCancel
    }



    /// <summary>
    /// Substitutes fro MessageBoxResult
    /// </summary>
    public enum MessageBoxResults
    {
        Cancel,
        No,
        None,
        OK,
        Yes
    }

    public static class MessageBoxConverter
    {
        public static MessageBoxButton ToBuiltinButtons(MessageBoxButtons buttons)
        {
            switch (buttons)
            {
                case MessageBoxButtons.OK: return MessageBoxButton.OK;
                case MessageBoxButtons.OKCancel: return MessageBoxButton.OKCancel;
                case MessageBoxButtons.YesNo: return MessageBoxButton.YesNo;
                case MessageBoxButtons.YesNoCancel: return MessageBoxButton.YesNoCancel;
                default: return MessageBoxButton.OK;
            }
        }

        public static MessageBoxResults FromBuiltInResults(MessageBoxResult result)
        {
            switch (result)
            {
                case MessageBoxResult.Yes: return MessageBoxResults.Yes;
                case MessageBoxResult.No: return MessageBoxResults.No;
                case MessageBoxResult.None: return MessageBoxResults.None;
                case MessageBoxResult.OK: return MessageBoxResults.OK;
                case MessageBoxResult.Cancel: return MessageBoxResults.Cancel;
                default: return MessageBoxResults.Cancel;
            }

        }

    }

}
