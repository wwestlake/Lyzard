/*
 * sukram: The Code Project Open License (CPOL)
 * https://www.codeproject.com/Articles/22952/WPF-Diagram-Designer-Part-1
 */

using System;

namespace Lyzard.CustomControls
{
    public class DesignerItemDeleteEventArgs : EventArgs
    {

        public DesignerItem Item { get; internal set; }

    }
}