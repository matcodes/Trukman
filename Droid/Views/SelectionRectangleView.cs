using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Text;

namespace Trukman.Droid
{
    public class SelectionRectangleView : View
    {
        #region Private members

        private Paint OutlinePaint = new Paint();
        private Paint InlinePaint = new Paint();

        private int startX = 0;
        private int startY = 0;
        private int endX = 0;
        private int endY = 0;
        private Boolean mDrawRect;

        private OnUpCallback mCallback = null;

        private Drawable resizeWidthBtn;
        private Drawable resizeHeightBtn;


        #endregion

        #region Constructors

        public SelectionRectangleView(Context context)
            : base(context)
        {
            Initialize();
        }

        public SelectionRectangleView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            Initialize();
        }

        public SelectionRectangleView(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {
            Initialize();
        }

        #endregion

        public interface OnUpCallback {
            void onRectFinished(Rect rect);
        }

        public void setOnUpCallback(OnUpCallback callback) {
            mCallback = callback;
        }

        private void Initialize()
        {
            resizeWidthBtn = Context.Resources.GetDrawable(Resource.Drawable.width_resizer);
            resizeHeightBtn = Context.Resources.GetDrawable(Resource.Drawable.height_resizer);

            InlinePaint.SetARGB(125, 59, 211, 219);
            OutlinePaint.SetARGB(255, 59, 211, 219);
            OutlinePaint.SetStyle(Paint.Style.Stroke);
            OutlinePaint.StrokeWidth = 3;
        }

        public override bool OnTouchEvent(MotionEvent e) {
            switch (e.Action) {
                case MotionEventActions.Down:
                    mDrawRect = false;
                    startX = (int)e.GetX();
                    startY = (int)e.GetY();
                    Invalidate();
                    break;

                case MotionEventActions.Move:
                    int x = (int)e.GetX();
                    int y = (int)e.GetY();

                    if (!mDrawRect || Math.Abs(x - endX) > 5 || Math.Abs(y - endY) > 5) {
                        endX = x;
                        endY = y;

                        Invalidate();
                    }

                    mDrawRect = true;
                    break;

                case MotionEventActions.Up:
                    if (mCallback != null) {
                        mCallback.onRectFinished(new Rect(Math.Min(startX, endX), Math.Min(startY, endY),
                                Math.Max(endX, startX), Math.Max(endY, startX)));
                    }
                    Invalidate();
                    break;

                default:
                    break;
            }

            return true;
        }
            
        protected override void OnDraw(Canvas canvas) {
            base.OnDraw(canvas);

            if (mDrawRect) {
                Rect rect = new Rect(Math.Min(startX, endX), Math.Min(startY, endY), Math.Max(endX, startX), Math.Max(endY, startY));
                canvas.DrawRect(rect, InlinePaint);
                canvas.DrawRect(rect, OutlinePaint);

                int xMiddle = rect.Left + ((rect.Right - rect.Left) / 2);
                int yMiddle = rect.Top + ((rect.Bottom - rect.Top) / 2);

                int btnHalfWidth = resizeWidthBtn.IntrinsicWidth / 2;
                int btnHalfHeight = resizeWidthBtn.IntrinsicHeight / 2;

                resizeWidthBtn.SetBounds(rect.Left - btnHalfWidth, yMiddle - btnHalfHeight, rect.Left + btnHalfWidth, yMiddle + btnHalfHeight);
                resizeWidthBtn.Draw(canvas);

                resizeWidthBtn.SetBounds(rect.Right - btnHalfWidth, yMiddle - btnHalfHeight, rect.Right + btnHalfWidth, yMiddle + btnHalfHeight);
                resizeWidthBtn.Draw(canvas);

                resizeHeightBtn.SetBounds(xMiddle - btnHalfWidth, rect.Top - btnHalfHeight, xMiddle + btnHalfWidth, rect.Top + btnHalfHeight);
                resizeHeightBtn.Draw(canvas);

                resizeHeightBtn.SetBounds(xMiddle - btnHalfWidth, rect.Bottom - btnHalfHeight, xMiddle + btnHalfWidth, rect.Bottom + btnHalfHeight);
                resizeHeightBtn.Draw(canvas);
            }
        }
    }
}

