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

namespace Lyzard.Interfaces
{
    /// <summary>
    /// Describes a log entry
    /// </summary>
    public enum LogEntryType
    {
        /// <summary>
        /// An INFO log message
        /// </summary>
        Info,

        /// <summary>
        /// A warning log message
        /// </summary>
        Warning,

        /// <summary>
        /// An error log message
        /// </summary>
        Error,

        /// <summary>
        /// An exception log message
        /// </summary>
        Exception,

        /// <summary>
        /// A critical log message
        /// </summary>
        Critical
    }


    /// <summary>
    /// Interface defining a logger
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Creates a log entry
        /// </summary>
        /// <param name="type">THe type of log entry</param>
        /// <param name="message">The message</param>
        /// <param name="args">Any arguments</param>
        void Log(LogEntryType type, string message, params object[] args);

        /// <summary>
        /// Creates an INFO log message
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="args">Any arguments</param>
        void LogInfo(string message, params object[] args);

        /// <summary>
        /// Creates a WARNING log message
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="args">Any arguments</param>
        void LogWarning(string message, params object[] args);

        /// <summary>
        /// Creates an ERROR log message
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="args">Any arguments</param>
        void LogError(string message, params object[] args);

        /// <summary>
        /// Creates an exception log message
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="args">Any arguments</param>
        void LogException(string message, params object[] args);

        /// <summary>
        /// Creates a critical log message
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="args">Any arguments</param>
        void LogCritical(string message, params object[] args);
    }
}
