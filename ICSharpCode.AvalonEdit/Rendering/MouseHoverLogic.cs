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
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace ICSharpCode.AvalonEdit.Rendering
{
	/// <summary>
	/// Encapsulates and adds MouseHover support to UIElements.
	/// </summary>
	public class MouseHoverLogic : IDisposable
	{
		UIElement target;
		
		DispatcherTimer mouseHoverTimer;
		Point mouseHoverStartPoint;
		MouseEventArgs mouseHoverLastEventArgs;
		bool mouseHovering;
		
		/// <summary>
		/// Creates a new instance and attaches itself to the <paramref name="target" /> UIElement.
		/// </summary>
		public MouseHoverLogic(UIElement target)
		{
			if (target == null)
				throw new ArgumentNullException("target");
			this.target = target;
			this.target.MouseLeave += MouseHoverLogicMouseLeave;
			this.target.MouseMove += MouseHoverLogicMouseMove;
			this.target.MouseEnter += MouseHoverLogicMouseEnter;
		}
		
		void MouseHoverLogicMouseMove(object sender, MouseEventArgs e)
		{
			Vector mouseMovement = mouseHoverStartPoint - e.GetPosition(this.target);
			if (Math.Abs(mouseMovement.X) > SystemParameters.MouseHoverWidth
			    || Math.Abs(mouseMovement.Y) > SystemParameters.MouseHoverHeight)
			{
				StartHovering(e);
			}
			// do not set e.Handled - allow others to also handle MouseMove
		}
		
		void MouseHoverLogicMouseEnter(object sender, MouseEventArgs e)
		{
			StartHovering(e);
			// do not set e.Handled - allow others to also handle MouseEnter
		}
		
		void StartHovering(MouseEventArgs e)
		{
			StopHovering();
			mouseHoverStartPoint = e.GetPosition(this.target);
			mouseHoverLastEventArgs = e;
			mouseHoverTimer = new DispatcherTimer(SystemParameters.MouseHoverTime, DispatcherPriority.Background, OnMouseHoverTimerElapsed, this.target.Dispatcher);
			mouseHoverTimer.Start();
		}
		
		void MouseHoverLogicMouseLeave(object sender, MouseEventArgs e)
		{
			StopHovering();
			// do not set e.Handled - allow others to also handle MouseLeave
		}
		
		void StopHovering()
		{
			if (mouseHoverTimer != null) {
				mouseHoverTimer.Stop();
				mouseHoverTimer = null;
			}
			if (mouseHovering) {
				mouseHovering = false;
				OnMouseHoverStopped(mouseHoverLastEventArgs);
			}
		}
		
		void OnMouseHoverTimerElapsed(object sender, EventArgs e)
		{
			mouseHoverTimer.Stop();
			mouseHoverTimer = null;
			
			mouseHovering = true;
			OnMouseHover(mouseHoverLastEventArgs);
		}
		
		/// <summary>
		/// Occurs when the mouse starts hovering over a certain location.
		/// </summary>
		public event EventHandler<MouseEventArgs> MouseHover;
		
		/// <summary>
		/// Raises the <see cref="MouseHover"/> event.
		/// </summary>
		protected virtual void OnMouseHover(MouseEventArgs e)
		{
			if (MouseHover != null) {
				MouseHover(this, e);
			}
		}
		
		/// <summary>
		/// Occurs when the mouse stops hovering over a certain location.
		/// </summary>
		public event EventHandler<MouseEventArgs> MouseHoverStopped;
		
		/// <summary>
		/// Raises the <see cref="MouseHoverStopped"/> event.
		/// </summary>
		protected virtual void OnMouseHoverStopped(MouseEventArgs e)
		{
			if (MouseHoverStopped != null) {
				MouseHoverStopped(this, e);
			}
		}
		
		bool disposed;
		
		/// <summary>
		/// Removes the MouseHover support from the target UIElement.
		/// </summary>
		public void Dispose()
		{
			if (!disposed) {
				this.target.MouseLeave -= MouseHoverLogicMouseLeave;
				this.target.MouseMove -= MouseHoverLogicMouseMove;
				this.target.MouseEnter -= MouseHoverLogicMouseEnter;
			}
			disposed = true;
		}
	}
}
