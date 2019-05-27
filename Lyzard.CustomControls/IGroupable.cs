/*
 * sukram: The Code Project Open License (CPOL)
 * https://www.codeproject.com/Articles/22952/WPF-Diagram-Designer-Part-1
 */

using System;

namespace Lyzard.CustomControls
{
    public interface IGroupable
    {
        Guid ID { get; }
        Guid ParentID { get; set; }
        bool IsGroup { get; set; }
    }
}
