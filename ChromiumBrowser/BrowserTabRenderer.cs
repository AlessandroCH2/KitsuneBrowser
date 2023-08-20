using KitsuneBrowser.Properties;
using EasyTabs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win32Interop.Enums;
using System.Drawing;
using System.Windows.Forms;
namespace KitsuneBrowser
{
    public class BrowserTabRenderer : ChromeTabRenderer
    {
        public BrowserTabRenderer(TitleBarTabs parentWindow) : base(parentWindow)
        {
            this._background = Resources.BackgroundTab;
          /*  this._inactiveLeftSideImage = Resources.LeftSide;
            this._inactiveRightSideImage = Resources.RightSide;
            this._inactiveCenterImage = Resources.TabCenter;
            this._activeCenterImage = Resources.TabCenter_Active;
            this._activeLeftSideImage = Resources.LeftSide_Active;
            this._activeRightSideImage = Resources.RightSide_Active;
            this._addButtonImage = Resources.PlusButton;
            this._addButtonHoverImage = Resources.PlusButton_Hover;*/


            this._inactiveLeftSideImage = Resources.InactiveLeft;
            this._inactiveRightSideImage = Resources.InactiveRight;
            this._inactiveCenterImage = Resources.InactiveCenter;
            this._activeCenterImage = Resources.Center;
            this._activeLeftSideImage = Resources.Left;
            this._activeRightSideImage = Resources.Right;
            this._addButtonImage = Resources.Add;
            this._addButtonHoverImage = Resources.AddHover;
            this._closeButtonImage = Resources.Close;
            this._closeButtonHoverImage = Resources.CloseHover;
            _windowsSizingBoxes = new WindowsSizingBoxesBlack(parentWindow);
            this.ForeColor = Color.White;

           
            /*  this.IconMarginLeft = 20;
              this.IconMarginTop = 8;
              this.CaptionMarginTop = 8;
              this.CaptionMarginRight = 5;
              this.AddButtonMarginLeft = -10;*/


        }
        public WindowsSizingBoxesBlack _windowsSizingBoxes = null;
        public override void Render(List<TitleBarTab> tabs, Graphics graphicsContext, Point offset, Point cursor, bool forceRedraw = false)
        {
            base.Render(tabs, graphicsContext, offset, cursor, forceRedraw);

            if (IsWindows10)
            {
               
                this._windowsSizingBoxes.Render(graphicsContext, cursor);
            }
        }
        public override bool IsOverSizingBox(Point cursor)
        {
            return _windowsSizingBoxes.Contains(cursor);
        }
        public override HT NonClientHitTest(Message message, Point cursor)
        {

            HT result = _windowsSizingBoxes.NonClientHitTest(cursor);
            
            return result == HT.HTNOWHERE ? HT.HTCAPTION : result;
        }
        
        protected override int GetMaxTabAreaWidth(List<TitleBarTab> tabs, Point offset)
        {
            return _parentWindow.ClientRectangle.Width - offset.X -
                        (ShowAddButton
                            ? _addButtonImage.Width + AddButtonMarginLeft + AddButtonMarginRight
                            : 0) -
                        (tabs.Count * OverlapWidth) -
                        _windowsSizingBoxes.Width;
        }
        /*  protected override int GetMaxTabAreaWidth(List<TitleBarTab> tabs, Point offset)
          {
              return _parentWindow.Width - offset.X - 200 -
                          (ShowAddButton
                              ? _addButtonImage.Width + AddButtonMarginLeft + AddButtonMarginRight
                              : 0);
          }
          public override int OverlapWidth
          {
              get
              {
                  return -6;
              }
          }*/

    }
}
