    class ListViewThatKeepsSelection : ListView
	{
		protected override void WndProc(ref Message m)
		{
			// Suppress mouse messages that are OUTSIDE of the items area
			if (m.Msg >= 0x201 && m.Msg <= 0x209)
			{
				Point pos = new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16);
				var hit = this.HitTest(pos);
				switch (hit.Location)
				{
					case ListViewHitTestLocations.AboveClientArea:
					case ListViewHitTestLocations.BelowClientArea:
					case ListViewHitTestLocations.LeftOfClientArea:
					case ListViewHitTestLocations.RightOfClientArea:
					case ListViewHitTestLocations.None:
						return;
				}
			}
			base.WndProc(ref m);
		}
	}
