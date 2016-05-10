using System;
using Android.Content;
using Android.Support.V4.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Util;
using KAS.Trukman.Droid;

namespace Trukman.Droid.Views
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
		private int lastTouchX = 0;
		private int lastTouchY = 0;
		private String resizeSide = null;
		private Boolean dragMode;
		private Boolean mDrawRect;

		private Drawable resizeWidthBtn;
		private Drawable resizeHeightBtn;


		#endregion

		#region Constructors
		public SelectionRectangleView(Context context)
			: base(context)
		{
			Initialize(context);
		}

		public SelectionRectangleView(Context context, IAttributeSet attrs)
			: base(context, attrs)
		{
			Initialize(context);
		}

		public SelectionRectangleView(Context context, IAttributeSet attrs, int defStyle)
			: base(context, attrs, defStyle)
		{
			Initialize(context);
		}
		#endregion


		public Rect getBounds() {
			return new Rect(Math.Min(startX, endX), Math.Min(startY, endY),
				Math.Max(endX, startX), Math.Max(endY, startY));
		}

		private void Initialize(Context ctx)
		{
			resizeWidthBtn = ContextCompat.GetDrawable(ctx, Resource.Drawable.width_resizer);
			resizeHeightBtn = ContextCompat.GetDrawable(ctx, Resource.Drawable.height_resizer);

			InlinePaint.SetARGB(125, 59, 211, 219);
			OutlinePaint.SetARGB(255, 59, 211, 219);
			OutlinePaint.SetStyle(Paint.Style.Stroke);
			OutlinePaint.StrokeWidth = 3;
		}

		public override bool OnTouchEvent(MotionEvent e) {
			int x = (int)e.GetX();
			int y = (int)e.GetY();
			switch (e.Action) {
			case MotionEventActions.Down:
				int resizeRange = 20;

				if ((((x >= startX - resizeRange && x <= startX + resizeRange) || 
					(x >= endX - resizeRange && x <= endX + resizeRange)) && ((y >= startY && y <= endY) || (y >= endY && y <= startY))) ||
					(((y >= startY - resizeRange && y <= startY + resizeRange) || 
						(y >= endY - resizeRange && y <= endY + resizeRange)) && ((x >= startX && x <= endX) || (x >= endX && x <= startX))))
				{
					bool horizontalCheck = ((x >= startX && x <= endX) || (x >= endX && x <= startX));
					bool verticalCheck = ((y >= startY && y <= endY) || (y >= endY && y <= startY));


					if (Math.Abs(startX - x) < resizeRange && verticalCheck)
					{
						resizeSide = "left";
					}
					if (Math.Abs(endX - x) < resizeRange && verticalCheck)
					{
						resizeSide = "right";
					}
					if (Math.Abs(startY - y) < resizeRange && horizontalCheck)
					{
						resizeSide = "top";
					}
					if (Math.Abs(endY - y) < resizeRange && horizontalCheck)
					{
						resizeSide = "bottom";
					}

					dragMode = false;
					lastTouchX = x;
					lastTouchY = y;

				}
				// Checking touch for right to left and left to right selection
				else if ((x > startX && x < endX &&
					y > startY && y < endY) ||
					(x > endX && x < startX &&
						y > endY && y < startY))
				{
					dragMode = true;
					resizeSide = null;
					lastTouchX = x;
					lastTouchY = y;
				}
				else
				{
					mDrawRect = false;
					dragMode = false;
					resizeSide = null;
					startX = x;
					startY = y;
				}

				Invalidate();
				break;

			case MotionEventActions.Move:
				if (!mDrawRect || Math.Abs(x - endX) > 5 || Math.Abs(y - endY) > 5) {
					if (dragMode)
					{
						int dx = x - lastTouchX;
						int dy = y - lastTouchY;

						startY += dy;
						startX += dx;
						endY += dy;
						endX += dx;
						lastTouchX = x;
						lastTouchY = y;
					}
					else if (resizeSide != null)
					{
						resize(x - lastTouchX, y - lastTouchY);

						lastTouchX = x;
						lastTouchY = y;
					}
					else
					{
						endX = x;
						endY = y;
					}

					Invalidate();
				}

				mDrawRect = true;
				break;

			case MotionEventActions.Up:
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

		private void resize(int dx, int dy) {
			switch (resizeSide)
			{
			case "left":
				startX += dx;
				break;
			case "right":
				endX += dx;
				break;
			case "top":
				startY += dy;
				break;
			case "bottom":
				endY += dy;
				break;
			};
		}
	}
}

