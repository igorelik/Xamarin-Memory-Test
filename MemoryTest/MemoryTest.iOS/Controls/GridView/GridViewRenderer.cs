using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using CoreGraphics;
using Foundation;
using iCareHealth.Mobile.iOS.Controls;
using MemoryTest.Controls;
using MemoryTest.iOS.Controls.GridView;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using PropertyChangingEventArgs = Xamarin.Forms.PropertyChangingEventArgs;

[assembly: ExportRenderer(typeof(GridView), typeof(GridViewRenderer))]
namespace MemoryTest.iOS.Controls.GridView
{
	

	/// <summary>
	/// Class GridViewRenderer.
	/// </summary>
	public class GridViewRenderer: ViewRenderer<MemoryTest.Controls.GridView,GridCollectionView>, IGridViewProvider
	{


		GridCollectionView _gridCollectionView;
		UIEdgeInsets _edgeInsets;
		int? _initialIndex;

		/// <summary>
		/// Called when [element changed].
		/// </summary>
		/// <param name="e">The e.</param>
		protected override void OnElementChanged (ElementChangedEventArgs<MemoryTest.Controls.GridView> e)
		{
			base.OnElementChanged (e);
			if (e.NewElement == null) {
				return;
			}
			e.NewElement.GridViewProvider = this;


		    _gridCollectionView = new GridCollectionView
		    {
		        AllowsMultipleSelection = false,
		        SelectionEnable = e.NewElement.SelectionEnabled,
		        BackgroundColor = Element.BackgroundColor.ToUIColor(),
		        ItemSize = new CGSize((float) Element.ItemWidth, (float) Element.ItemHeight),
		        RowSpacing = Element.RowSpacing,
		        ColumnSpacing = Element.ColumnSpacing
		    };

		    if (e.NewElement.IsHorizontal) {
				var flowLayout = (UICollectionViewFlowLayout)_gridCollectionView.CollectionViewLayout;
				flowLayout.ScrollDirection = UICollectionViewScrollDirection.Horizontal;
			}
			Unbind (e.OldElement);
			Bind (e.NewElement);

			_gridCollectionView.Source = (e.NewElement.ItemsSource != null) ? DataSource : null;
			_gridCollectionView.Delegate = new GridViewDelegate (ItemSelected, HandleOnScrolled);
			ScrollToInitialIndex ();
			InvalidatePadding ();

			SetNativeControl (_gridCollectionView);
		}

	    private bool IsPaddingInvalid { get; set; }

	    void InvalidatePadding ()
		{
			IsPaddingInvalid = true;
			SetNeedsLayout ();
		}

		private nfloat _previousWidth;

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			_gridCollectionView.Frame = Bounds;
			var widthChanged = _previousWidth != _gridCollectionView.Frame.Width;
			if (widthChanged) {
				_previousWidth = _gridCollectionView.Frame.Width;
			}
			if (IsPaddingInvalid || widthChanged) {
				UpdatePadding ();
			}
		}

		/// <summary>
		/// Raises the element property changed event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		protected override void OnElementPropertyChanged (object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);
			if (e.PropertyName == "ItemsSource") {
				var gridView = sender as MemoryTest.Controls.GridView;
				if (gridView != null && gridView.ItemsSource != null) {
					_gridCollectionView.Source = DataSource;
					ReloadData ();
					ScrollToInitialIndex ();
				}
			}

		    if (e.PropertyName == "IsScrollEnabled")
		    {
		        Device.BeginInvokeOnMainThread(() =>
		        {
		            _gridCollectionView.ScrollEnabled = Element.IsScrollEnabled;
		            Debug.WriteLine("scroll enabled changed to " + _gridCollectionView.ScrollEnabled);
		        });
		    }

		    if (e.PropertyName == "ItemSize")
		    {
		        var gridView = sender as MemoryTest.Controls.GridView;
		        if (gridView != null)
		        {
		            _gridCollectionView.ItemSize = gridView.ItemSize.ToSizeF();
		        }
		        InvalidatePadding();
		    }
		    if (e.PropertyName == "IsHorizontal") {
				var flowLayout = (UICollectionViewFlowLayout)_gridCollectionView.CollectionViewLayout;
				flowLayout.ScrollDirection = Element.IsHorizontal ? UICollectionViewScrollDirection.Horizontal : UICollectionViewScrollDirection.Vertical;
			}
			if (e.PropertyName == "IsContentCentered") {
				InvalidatePadding ();
			}
			if (e.PropertyName == "ContentPaddingLeft") {
				InvalidatePadding ();
			}
			if (e.PropertyName == "ContentPaddingTop") {
				InvalidatePadding ();
			}
			if (e.PropertyName == "ContentPaddingBottom") {
				InvalidatePadding ();
			}
			if (e.PropertyName == "CenterAsFilledRow") {
				InvalidatePadding ();
			}
			if (e.PropertyName == "ContentPaddingRight") {
				InvalidatePadding ();
			}
			if (e.PropertyName == "SectionPaddingBottom") {
				InvalidatePadding ();
			}	
			if (e.PropertyName == "MaxItemsPerRow") {
				InvalidatePadding ();
			}
			if (e.PropertyName == "SectionPaddingTop") {
				InvalidatePadding ();
			}
		}

	    /// <summary>
	    /// Unbinds the specified old element.
	    /// </summary>
	    /// <param name="oldElement">The old element.</param>
	    private void Unbind(MemoryTest.Controls.GridView oldElement)
	    {
	        if (oldElement != null)
	        {
	            oldElement.PropertyChanging -= ElementPropertyChanging;
	            oldElement.PropertyChanged -= ElementPropertyChanged;

	            var itemsSource = oldElement.ItemsSource as INotifyCollectionChanged;
	            if (itemsSource != null)
	            {
	                itemsSource.CollectionChanged -= DataCollectionChanged;
	            }
	        }
	    }

	    /// <summary>
	    /// Binds the specified new element.
	    /// </summary>
	    /// <param name="newElement">The new element.</param>
	    private void Bind(MemoryTest.Controls.GridView newElement)
	    {
	        if (newElement != null)
	        {
	            newElement.PropertyChanging += ElementPropertyChanging;
	            newElement.PropertyChanged += ElementPropertyChanged;
	            if (newElement.ItemsSource is INotifyCollectionChanged)
	            {
	                ((INotifyCollectionChanged) newElement.ItemsSource).CollectionChanged += DataCollectionChanged;
	            }
	        }
	    }

	    /// <summary>
		/// Elements the property changed.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
		private void ElementPropertyChanged (object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "ItemsSource") {
				var itemsSource = Element != null ? Element.ItemsSource as INotifyCollectionChanged : null;
				if (itemsSource != null) {
					itemsSource.CollectionChanged -= DataCollectionChanged;
				}
			}
		}

		/// <summary>
		/// Elements the property changing.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="PropertyChangingEventArgs"/> instance containing the event data.</param>
		private void ElementPropertyChanging (object sender, PropertyChangingEventArgs e)
		{
			if (e.PropertyName == "ItemsSource") {
				var itemsSource = Element != null ? Element.ItemsSource as INotifyCollectionChanged : null;
				if (itemsSource != null) {
					itemsSource.CollectionChanged += DataCollectionChanged;
				}
			}
		}

	    /// <summary>
	    /// Datas the collection changed.
	    /// </summary>
	    /// <param name="sender">The sender.</param>
	    /// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
	    private void DataCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
	    {
	        try
	        {
	            if (Control != null)
	            {
	                ReloadData();
	            }
	        }
	        catch (Exception ex)
	        {
	            Console.WriteLine("error " + ex.Message);
	        }
	    }

	    /// <summary>
		/// The _data source
		/// </summary>
		private GridDataSource _dataSource;

		/// <summary>
		/// Gets the data source.
		/// </summary>
		/// <value>The data source.</value>
		private GridDataSource DataSource {
			get {
				return _dataSource ??
					(_dataSource =
						new GridDataSource (GetCell, RowsInSection, ItemSelected));
			}
		}

		void HandleOnScrolled (CGPoint contentOffset)
		{
		    if (_gridCollectionView != null && _gridCollectionView.VisibleCells != null)
		    {
		        foreach (var nativeCell in _gridCollectionView.VisibleCells.Cast<GridViewCell>()
                                                            .Where(nativeCell => nativeCell != null))
		        {
		            nativeCell.ViewCell.OnScroll(contentOffset.ToPoint(), new Point(nativeCell.Frame.X, nativeCell.Frame.Y));
		        }
		        Element.RaiseOnScroll(0, (float) contentOffset.Y);
		    }
		}

	    void ScrollToInitialIndex ()
		{
			if (_initialIndex.HasValue && _gridCollectionView != null && _gridCollectionView.DataSource != null) {
				ScrollToItemWithIndex (_initialIndex.Value, false);
				_initialIndex = null;
			}
		}

		/// <summary>
		/// Rowses the in section.
		/// </summary>
		/// <param name="collectionView">The collection view.</param>
		/// <param name="section">The section.</param>
		/// <returns>System.Int32.</returns>
		public int RowsInSection (UICollectionView collectionView, nint section)
		{
			//			var property = Element.ItemsSource.GetType ().GetProperty ("InstanceId");
			//			string instanceId = property?.GetValue (Element.ItemsSource)?.ToString ();
			//			Console.WriteLine (">>>>> countfrom  collection {0} is {1}", instanceId, ((ICollection)Element.ItemsSource).Count);
			var numberOfItems = ((ICollection)Element.ItemsSource).Count;
			return numberOfItems;
		}

		/// <summary>
		/// Items the selected.
		/// </summary>
		/// <param name="tableView">The table view.</param>
		/// <param name="indexPath">The index path.</param>
		public void ItemSelected (UICollectionView tableView, NSIndexPath indexPath)
		{
			var item = Element.ItemsSource.Cast<object> ().ElementAt (indexPath.Row);
			Element.InvokeItemSelectedEvent (this, item);
		}

		private NSString _cellId;

		/// <summary>
		/// Gets the cell.
		/// </summary>
		/// <returns>The cell.</returns>
		/// <param name="collectionView">Collection view.</param>
		/// <param name="indexPath">Index path.</param>
		public UICollectionViewCell GetCell (UICollectionView collectionView, NSIndexPath indexPath)
		{
			_cellId = _cellId ?? new NSString (GridViewCell.Key);
			var item = Element.ItemsSource.Cast<object> ().ElementAt (indexPath.Row);
			var collectionCell = collectionView.DequeueReusableCell (_cellId, indexPath) as GridViewCell;
		    if (collectionCell != null)
		    {
		        collectionCell.RecycleCell(item, Element.ItemTemplate, Element);
		    }
		    return collectionCell;
		}


		/// <summary>
		/// Reloads the data.
		/// </summary>
		public void ReloadData ()
		{
			if (_gridCollectionView != null) {
				InvokeOnMainThread (() => {
					UpdatePadding ();
					_gridCollectionView.ReloadData ();
					_gridCollectionView.Delegate = new GridViewDelegate (ItemSelected, HandleOnScrolled);
				}
				);
			}
		}


		//TODO this method/mechanism needs some more thought
		/// <summary>
		/// Scrolls the index of the to item with.
		/// </summary>
		/// <param name="index">Index.</param>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public void ScrollToItemWithIndex (int index, bool animated)
		{
			if (_gridCollectionView != null && _gridCollectionView.NumberOfItemsInSection (0) > index) {
				var indexPath = NSIndexPath.FromRowSection (index, 0);
				InvokeOnMainThread (() => {
					_gridCollectionView.ScrollToItem (indexPath, UICollectionViewScrollPosition.Top, animated);
				});
			} else {
				_initialIndex = index;
			}
		}

		/// <summary>
		/// Updates the padding for when we center the content in the gridview
		/// </summary>
		void UpdatePadding ()
		{
			if (Element == null || (ICollection)Element.ItemsSource == null) {
				return;
			}
			var numberOfItems = ((ICollection)Element.ItemsSource).Count;
			UICollectionViewFlowLayout flowLayout = _gridCollectionView != null ? (UICollectionViewFlowLayout)_gridCollectionView.CollectionViewLayout : null;
			if (flowLayout != null) {
				if (Element.IsContentCentered && numberOfItems > 0 && _gridCollectionView.Frame.Width > 0) {
					flowLayout.InvalidateLayout ();
					var width = (float)_gridCollectionView.Frame.Width - 2;
					var numberOfItemsThatFit = (int)Math.Floor (width / (_gridCollectionView.ItemSize.Width));
					var numberOfItemsToUse = Element.CenterAsFilledRow ? numberOfItemsThatFit : Math.Min (numberOfItemsThatFit, numberOfItems);
					if (Element.MaxItemsPerRow != -1) {
						numberOfItemsToUse = Element.MaxItemsPerRow;
					}
					var remainingWidth = width - (numberOfItemsToUse * (_gridCollectionView.ItemSize.Width));
					var padding = remainingWidth / (numberOfItemsToUse + 1);

					Console.WriteLine (" width {0} items using {1} padding {2} iwdith {3} ", _gridCollectionView.Frame.Width, numberOfItemsToUse, padding, _gridCollectionView.ItemSize.Width);
					_gridCollectionView.ColumnSpacing = padding;
					_edgeInsets = new UIEdgeInsets ((float)Element.ContentPaddingTop, (float)padding, (float)Element.ContentPaddingBottom, (float)padding);
					Console.WriteLine ("final insets " + _edgeInsets);
					_gridCollectionView.ContentInset = _edgeInsets;
				}
				flowLayout.SectionInset = new UIEdgeInsets ((float)Element.SectionPaddingTop, 0, (float)Element.SectionPaddingBottom, 0);
				if (_gridCollectionView.Frame.Width > 0 && _gridCollectionView.Frame.Height > 0) {
					IsPaddingInvalid = false;
				}
			}
		}

        protected override void Dispose(bool disposing)
        {
            _gridCollectionView.Dispose();
            base.Dispose(disposing);
        }        
    }
}