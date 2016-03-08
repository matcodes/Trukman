using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Text;

namespace Trukman.Droid
{
    public class DragRect : View
    {
        #region Private members
        private Paint mRectPaint;

        private int startX = 0;
        private int startY = 0;
        private int endX = 0;
        private int endY = 0;
        private Boolean mDrawRect;

        private OnUpCallback mCallback = null;

        #endregion

        #region Constructors

        public DragRect(Context context)
            : base(context)
        {
            Initialize();
        }

        public DragRect(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            Initialize();
        }

        public DragRect(Context context, IAttributeSet attrs, int defStyle)
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
            mRectPaint = new Paint();
            mRectPaint.SetARGB(255, 255, 0, 0);
            mRectPaint.SetStyle(Paint.Style.Stroke);
            mRectPaint.StrokeWidth = 5;
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
                    int x = (int) e.GetX();
                    int y = (int) e.GetY();

                    if (!mDrawRect || Math.Abs(x - endX) > 5 || Math.Abs(y - endY) > 5) {
                        endX = x;
                        endY = y;
                        Invalidate();
                    }

                    mDrawRect = true;
                    break;

                case MotionEventActions.Up:
                    if (mCallback != null)
                    {
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
                canvas.DrawRect(Math.Min(startX, endX), Math.Min(startY, endY),
                    Math.Max(endX, startX), Math.Max(endY, startY), mRectPaint);
            }
        }
    }
}

