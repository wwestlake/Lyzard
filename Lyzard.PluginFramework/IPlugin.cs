﻿/* 
 * Lyzard Code Generation System
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

namespace Lyzard.PluginFramework
{
    /// <summary>
    /// The main class of the plugin implements this interface.
    /// More than one class may implement this interface but the
    /// startup class should be specified in the Plugin.json file.
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Initializes the plugin
        /// </summary>
        /// <param name="api">The application API used to create documents and tool panes</param>
        void Initialize(IApplicationApi api);
    }
}