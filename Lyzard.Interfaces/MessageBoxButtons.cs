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
