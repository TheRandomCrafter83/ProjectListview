Public Class cListView
    Inherits ListView

    '// Function used to set Vista-theming on our listview
    <System.Runtime.InteropServices.DllImport("uxtheme", CharSet:=System.Runtime.InteropServices.CharSet.Unicode)>
    Public Shared Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal textSubAppName As String, ByVal textSubIdList As String) As Integer
    End Function


    'API Declarations
    Private Declare Sub CoInitialize Lib "ole32.dll" (ByVal pvReserved As Int32)
    Private Declare Sub CoUninitialize Lib "ole32.dll" ()
    Private Declare Function SendMessage Lib "user32.dll" Alias "SendMessageA" (ByVal hwnd As Int32, ByVal wMsg As Int32, ByVal wParam As Int32, ByVal lParam As Int64) As Int32


    'Structure needed to set the listviews background watermark image
    Public Structure LVBKIMAGE
        Public ulFlags As Int32
        Public hbm As IntPtr
        Public pszImage As String
        Public cchImageMax As Int32
        Public xOffsetPercent As Int32
        Public yOffsetPercent As Int32
    End Structure

    Private Const WM_CHANGEUISTATE As Integer = &H127
    Private Const UIS_SET As Integer = 1
    Private Const UISF_HIDEFOCUS As Integer = &H1
    Private Const UISF_ACTIVE As Integer = &H4

    'Constant Declarations
    Private Const LVM_FIRST As Int32 = &H1000
    Private Const LVM_SETBKIMAGEW As Int32 = (LVM_FIRST + 138)
    Private Const LVBKIF_TYPE_WATERMARK As Int32 = &H10000000

#Region "Column Header API"
    <Runtime.InteropServices.StructLayout(Runtime.InteropServices.LayoutKind.Sequential)> _
    Private Structure HDITEM
        Public mask As Int32
        Private ReadOnly cxy As Int32
        <Runtime.InteropServices.MarshalAs(Runtime.InteropServices.UnmanagedType.LPTStr)> Private pszText As [String]
        Private ReadOnly hbm As IntPtr
        Private ReadOnly cchTextMax As Int32
        Public fmt As Int32
        Private ReadOnly lParam As Int32
        Private ReadOnly iImage As Int32
        Private ReadOnly iOrder As Int32
    End Structure

    <Runtime.InteropServices.DllImport("user32", EntryPoint:="SendMessage")> _
    Private Shared Function SendMessage2(ByVal Handle As IntPtr, ByVal msg As Int32, ByVal wParam As IntPtr, ByRef lParam As HDITEM) As IntPtr
    End Function

    Const HDI_WIDTH As Int32 = &H1
    Const HDI_HEIGHT As Int32 = HDI_WIDTH
    Const HDI_TEXT As Int32 = &H2
    Const HDI_FORMAT As Int32 = &H4
    Const HDI_LPARAM As Int32 = &H8
    Const HDI_BITMAP As Int32 = &H10
    Const HDI_IMAGE As Int32 = &H20
    Const HDI_DI_SETITEM As Int32 = &H40
    Const HDI_ORDER As Int32 = &H80
    Const HDI_FILTER As Int32 = &H100
    Const HDF_LEFT As Int32 = &H0
    Const HDF_RIGHT As Int32 = &H1
    Const HDF_CENTER As Int32 = &H2
    Const HDF_JUSTIFYMASK As Int32 = &H3
    Const HDF_RTLREADING As Int32 = &H4
    Const HDF_OWNERDRAW As Int32 = &H8000
    Const HDF_STRING As Int32 = &H4000
    Const HDF_BITMAP As Int32 = &H2000
    Const HDF_BITMAP_ON_RIGHT As Int32 = &H1000
    Const HDF_IMAGE As Int32 = &H800
    Const HDF_SORTUP As Int32 = &H400
    Const HDF_SORTDOWN As Int32 = &H200
    ' List messages
    Const LVM_GETHEADER As Int32 = LVM_FIRST + 31
    Const HDM_FIRST As Int32 = &H1200
    ' Header messages
    Const HDM_SETIMAGELIST As Int32 = HDM_FIRST + 8
    Const HDM_GETIMAGELIST As Int32 = HDM_FIRST + 9
    Const HDM_GETITEM As Int32 = HDM_FIRST + 11
    Const HDM_SETITEM As Int32 = HDM_FIRST + 12

#End Region

#Region "ShowHeaderIcon"

    Private Shared Sub ShowHeaderIcon(ByVal list As ListView, ByVal columnIndex As Integer, ByVal sortOrder__1 As SortOrder)
        If columnIndex < 0 OrElse columnIndex >= list.Columns.Count Then
            Return
        End If

        Dim hHeader As IntPtr = SendMessage(list.Handle, LVM_GETHEADER, IntPtr.Zero, IntPtr.Zero)
        Dim colHdr As ColumnHeader = list.Columns(columnIndex)
        Dim hd As New HDITEM()

        hd.mask = HDI_FORMAT

        Dim align As HorizontalAlignment = colHdr.TextAlign

        If align = HorizontalAlignment.Left Then
            hd.fmt = HDF_LEFT Or HDF_STRING Or HDF_BITMAP_ON_RIGHT
        ElseIf align = HorizontalAlignment.Center Then
            hd.fmt = HDF_CENTER Or HDF_STRING Or HDF_BITMAP_ON_RIGHT
        Else
            ' HorizontalAlignment.Right
            hd.fmt = HDF_RIGHT Or HDF_STRING
        End If

        If sortOrder__1 = SortOrder.Ascending Then
            hd.fmt = hd.fmt Or HDF_SORTUP
        ElseIf sortOrder__1 = SortOrder.Descending Then
            hd.fmt = hd.fmt Or HDF_SORTDOWN
        End If

        SendMessage2(hHeader, HDM_SETITEM, New IntPtr(columnIndex), hd)
    End Sub

#End Region


    Event SubItemClick(ByVal sender As Object, ByVal e As SubItemClickEventArgs)
    Event SubItemDoubleClick(ByVal sender As Object, ByVal e As SubItemClickEventArgs)

    Dim FirstRun As Boolean
    Private ReadOnly lvwColumnSorter As SvenSo.ListViewColumnSorter = Nothing

    Dim vWatermarkImage As Bitmap
    Dim vWatermarkAlpha As Integer
    Dim vUseVistaThemeing As Boolean
    Dim vShowFocusRectangle As Boolean
    Dim vUseLedgerStyle As Boolean
    Dim vLedgerStyleLightColor As Color
    Dim vLedgerStyleDarkColor As Color
    Dim vColumnSortStyle As SvenSo.ListViewColumnSorter.SortModifiers
    Dim vColumnSorting As Boolean

    ''' <value>
    ''' Returns true on Windows Vista or newer operating systems; otherwise, false.
    ''' </value>
    Private ReadOnly Property IsVistaOrLater() As Boolean
        Get
            Return Environment.OSVersion.Platform = PlatformID.Win32NT AndAlso Environment.OSVersion.Version.Major >= 6
        End Get
    End Property

    Public Property UseLedgerStyle As Boolean
        Get
            Return vUseLedgerStyle
        End Get
        Set(ByVal value As Boolean)
            vUseLedgerStyle = value
            Me.Invalidate()
        End Set
    End Property

    Public Property LedgerStyleDarkColor As Color
        Get
            Return vLedgerStyleDarkColor
        End Get
        Set(ByVal value As Color)
            vLedgerStyleDarkColor = value
            Me.Invalidate()
        End Set
    End Property

    Public Property LedgerStyleLightColor As Color
        Get
            Return vLedgerStyleLightColor
        End Get
        Set(ByVal value As Color)
            vLedgerStyleLightColor = value
            Me.Invalidate()
        End Set
    End Property

    Public Property ColumnSortStyle() As SvenSo.ListViewColumnSorter.SortModifiers
        Get
            Return vColumnSortStyle
        End Get
        Set(ByVal value As SvenSo.ListViewColumnSorter.SortModifiers)
            vColumnSortStyle = value
            lvwColumnSorter._SortModifier = value
            For Each c As ColumnHeader In Me.Columns
                ShowHeaderIcon(Me, c.Index, SortOrder.None)
            Next
            ShowHeaderIcon(Me, lvwColumnSorter.ColumnToSort, lvwColumnSorter.OrderOfSort)
        End Set
    End Property

    Public Property ColumnSortIndex() As Integer
        Get
            Return lvwColumnSorter.ColumnToSort
        End Get
        Set(ByVal value As Integer)
            lvwColumnSorter.ColumnToSort = value
            For Each c As ColumnHeader In Me.Columns
                ShowHeaderIcon(Me, c.Index, SortOrder.None)
            Next
            ShowHeaderIcon(Me, lvwColumnSorter.ColumnToSort, lvwColumnSorter.OrderOfSort)
        End Set
    End Property

    Public Property ColumnSorting As Boolean
        Get
            Return vColumnSorting
        End Get
        Set(ByVal value As Boolean)
            vColumnSorting = value
            For Each c As ColumnHeader In Me.Columns
                ShowHeaderIcon(Me, c.Index, SortOrder.None)
            Next
            ShowHeaderIcon(Me, lvwColumnSorter.ColumnToSort, lvwColumnSorter.OrderOfSort)
        End Set
    End Property

    Public Shadows Property Sorting As SortOrder
        Get
            Return ColumnSortOrder
        End Get
        Set(ByVal value As SortOrder)
            Me.ColumnSortOrder = value
        End Set
    End Property

    Public Property ColumnSortOrder As SortOrder
        Get
            If lvwColumnSorter IsNot Nothing Then
                Return Me.lvwColumnSorter.OrderOfSort
            Else
                Return MyBase.Sorting
            End If
        End Get
        Set(ByVal value As SortOrder)
            If lvwColumnSorter IsNot Nothing Then
                Me.lvwColumnSorter.OrderOfSort = value
            End If
            MyBase.Sorting = value
            For Each c As ColumnHeader In Me.Columns
                ShowHeaderIcon(Me, c.Index, SortOrder.None)
            Next
            ShowHeaderIcon(Me, Me.ColumnSortIndex, MyBase.Sorting)
        End Set
    End Property

    Public Property ShowFocusRectangle As Boolean
        Get
            Return vShowFocusRectangle
        End Get
        Set(ByVal value As Boolean)
            vShowFocusRectangle = value
            If Not value Then
                SendMessage(Me.Handle, WM_CHANGEUISTATE, MakeLong(UIS_SET, UISF_HIDEFOCUS), 0)
            Else
                SendMessage(Me.Handle, WM_CHANGEUISTATE, MakeLong(UIS_SET, UISF_ACTIVE), 0)
            End If
        End Set
    End Property

    Public Property UseVistaThemeing As Boolean
        Get
            Return vUseVistaThemeing
        End Get
        Set(ByVal value As Boolean)
            vUseVistaThemeing = value
            If IsVistaOrLater And value Then
                SetWindowTheme(Handle, "explorer", Nothing)
            Else
                SetWindowTheme(Handle, Nothing, Nothing)
            End If
        End Set
    End Property

    <Configuration.DefaultSettingValue("200")> _
    Public Property WatermarkAlpha() As Integer
        Get
            Return vWatermarkAlpha
        End Get
        Set(ByVal value As Integer)
            vWatermarkAlpha = value
            SetBkImage()
        End Set
    End Property

    Public Property WatermarkImage() As Bitmap
        Get
            Return vWatermarkImage
        End Get
        Set(ByVal value As Bitmap)
            vWatermarkImage = value
            SetBkImage()
        End Set
    End Property

    Private Function MakeLong(ByVal wLow As Integer, ByVal wHigh As Integer) As Integer
        Dim low As Integer = CType(IntLoWord(wLow), Integer)
        Dim high As Short = IntLoWord(wHigh)
        Dim product As Integer = &H10000 * CType(high, Integer)
        Dim mkLong As Integer = CType((low Or product), Integer)
        Return mkLong
    End Function

    Private Function IntLoWord(ByVal word As Integer) As Short
        Return CType((word And Short.MaxValue), Short)
    End Function

    Private Sub SetBkImage()
        'Try
        If Not WatermarkImage Is Nothing Then
                Dim hBMP As IntPtr = GetBMP(WatermarkImage)
                If Not hBMP = IntPtr.Zero Then
                    Dim lv As New LVBKIMAGE
                    lv.hbm = hBMP
                    lv.ulFlags = LVBKIF_TYPE_WATERMARK
                    Dim lvPTR As IntPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(System.Runtime.InteropServices.Marshal.SizeOf(lv))
                    System.Runtime.InteropServices.Marshal.StructureToPtr(lv, lvPTR, False)
                    SendMessage(Me.Handle, LVM_SETBKIMAGEW, 0, lvPTR)
                    System.Runtime.InteropServices.Marshal.FreeCoTaskMem(lvPTR)
                End If
            Else
                Dim lv As New LVBKIMAGE
                lv.hbm = IntPtr.Zero
                lv.ulFlags = LVBKIF_TYPE_WATERMARK
                Dim lvPTR As IntPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(System.Runtime.InteropServices.Marshal.SizeOf(lv))
                System.Runtime.InteropServices.Marshal.StructureToPtr(lv, lvPTR, False)
                SendMessage(Me.Handle, LVM_SETBKIMAGEW, 0, lvPTR)
                System.Runtime.InteropServices.Marshal.FreeCoTaskMem(lvPTR)
            End If
        'Catch e As Exception
        '    MsgBox(e.ToString())
        'End Try
    End Sub

    Private Function GetBMP(ByVal FromImage As Image) As IntPtr
        Dim bmp As Bitmap = New Bitmap(FromImage.Width, FromImage.Height)
        Dim g As Graphics = Graphics.FromImage(bmp)
        g.Clear(Me.BackColor)
        g.DrawImage(FromImage, 0, 0, bmp.Width, bmp.Height)
        g.FillRectangle(New SolidBrush(Color.FromArgb(WatermarkAlpha, Me.BackColor.R, Me.BackColor.G, Me.BackColor.B)), New RectangleF(0, 0, bmp.Width, bmp.Height))
        g.Dispose()
        Return bmp.GetHbitmap
        bmp.Dispose()
    End Function

    Public Sub New()
        MyBase.New()
        lvwColumnSorter = New SvenSo.ListViewColumnSorter
        Me.ListViewItemSorter = lvwColumnSorter
        Me.AutoArrange = True
        lvwColumnSorter._SortModifier = vColumnSortStyle

        LedgerStyleDarkColor = Color.LightGray
        LedgerStyleLightColor = Color.White
        UseLedgerStyle = False
        FirstRun = True
        Me.OwnerDraw = True
        Me.WatermarkAlpha = 200
        MyBase.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        CoInitialize(IntPtr.Zero)
        SetBkImage()
        AddHandler Me.ColumnClick, AddressOf Me._ColumnClick
    End Sub

    Protected Overrides Sub OnNotifyMessage(ByVal m As System.Windows.Forms.Message)
        If (m.Msg <> &H14) Then
            MyBase.OnNotifyMessage(m)
        End If
    End Sub

    Private Sub _ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs)
        If vColumnSorting Then
            Dim myListView As ListView = DirectCast(sender, ListView)

            For i As Integer = 0 To Me.Columns.Count - 1
                ShowHeaderIcon(Me, i, SortOrder.None)
            Next

            ' Determine if clicked column is already the column that is being sorted.
            If e.Column = lvwColumnSorter.ColumnToSort Then
                ' Reverse the current sort direction for this column.
                If lvwColumnSorter.OrderOfSort = SortOrder.Ascending Then
                    lvwColumnSorter.OrderOfSort = SortOrder.Descending
                    ShowHeaderIcon(Me, e.Column, SortOrder.Descending)
                Else
                    lvwColumnSorter.OrderOfSort = SortOrder.Ascending
                    ShowHeaderIcon(Me, e.Column, SortOrder.Ascending)
                End If
            Else
                ' Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.ColumnToSort = e.Column
                lvwColumnSorter.OrderOfSort = SortOrder.Ascending
                ShowHeaderIcon(Me, e.Column, SortOrder.Ascending)
            End If
            ' Perform the sort with these new sort options.
            myListView.Sort()
        End If
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        CoUninitialize()
    End Sub

    Private Sub cListView_DrawColumnHeader(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawListViewColumnHeaderEventArgs) Handles Me.DrawColumnHeader
        '_ColumnClick(Me, New ColumnClickEventArgs(e.ColumnIndex))
        e.DrawDefault = True
    End Sub

    Private Sub cListView_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawListViewItemEventArgs) Handles Me.DrawItem
        If WatermarkImage IsNot Nothing Then
            e.DrawDefault = True
        End If
        If Not Me.View = Windows.Forms.View.Details Then
            e.DrawDefault = True
        Else
            If Me.UseLedgerStyle Then
                Dim brush As SolidBrush
                If WatermarkImage IsNot Nothing Then
                    If e.ItemIndex Mod 2 <> 0 Then
                        brush = New SolidBrush(Color.FromArgb(100, Me.LedgerStyleDarkColor.R, LedgerStyleDarkColor.G, LedgerStyleDarkColor.B))
                    Else
                        brush = New SolidBrush(Color.FromArgb(100, Me.LedgerStyleLightColor.R, LedgerStyleLightColor.G, LedgerStyleLightColor.B))
                    End If
                Else
                    If e.ItemIndex Mod 2 <> 0 Then
                        brush = New SolidBrush(LedgerStyleDarkColor)
                    Else
                        brush = New SolidBrush(LedgerStyleLightColor)
                    End If
                End If
                Try
                    If e.Bounds.Width < Me.ClientRectangle.Width Then
                        e.Graphics.FillRectangle(brush, New Rectangle(e.Bounds.X, e.Bounds.Y, ClientRectangle.Width, e.Bounds.Height))
                    Else
                        e.Graphics.FillRectangle(brush, e.Bounds)
                    End If

                Finally
                    brush.Dispose()
                End Try
            Else
                Dim brush As SolidBrush

                If WatermarkImage IsNot Nothing Then
                    brush = New SolidBrush(Color.FromArgb(100, e.Item.BackColor.R, e.Item.BackColor.G, e.Item.BackColor.B))
                Else
                    brush = New SolidBrush(e.Item.BackColor)
                End If

                Try
                    If e.Bounds.Width < Me.ClientRectangle.Width Then
                        e.Graphics.FillRectangle(brush, New Rectangle(e.Bounds.X, e.Bounds.Y, ClientRectangle.Width, e.Bounds.Height))
                    Else
                        e.Graphics.FillRectangle(brush, e.Bounds)
                    End If

                Finally
                    brush.Dispose()
                End Try
            End If
            If Not (e.State And ListViewItemStates.Selected) = 0 Then
                '    e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds)
                e.DrawFocusRectangle()
            End If
        End If
    End Sub

    Private Sub cListView_DrawSubItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawListViewSubItemEventArgs) Handles Me.DrawSubItem
        If WatermarkImage IsNot Nothing Then
            e.DrawDefault = True
        Else
            Dim flags As TextFormatFlags = TextFormatFlags.Left
            Dim sf As New StringFormat()

            Try
                Select Case e.Header.TextAlign
                    Case HorizontalAlignment.Center
                        sf.Alignment = StringAlignment.Center
                        flags = TextFormatFlags.HorizontalCenter
                    Case HorizontalAlignment.Right
                        sf.Alignment = StringAlignment.Far
                        flags = TextFormatFlags.Right
                End Select

                'e.DrawText()
                Dim RC As RectangleF = New RectangleF(e.Bounds.X + 3, e.Bounds.Y + 2, e.Bounds.Width, e.Bounds.Height)
                Try
                    e.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.SystemDefault
                    e.Graphics.DrawString(e.SubItem.Text, e.SubItem.Font, SystemBrushes.WindowText, RC, sf)
                Catch ex As Exception
                End Try

                'e.DrawDefault = True

                '''''NOTE:''''''''''''''''''''''''''''''
                'any special text formatting goes here''
                ''''''''''''''''''''''''''''''''''''''''
            Finally
                sf.Dispose()
            End Try

        End If
    End Sub


    Protected Overrides Sub OnMouseClick(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseClick(e)
        Dim hti As ListViewHitTestInfo = Me.HitTest(e.Location)
        If hti.Item.SubItems.IndexOf(hti.SubItem) > 0 Then
            RaiseEvent SubItemClick(Me, New SubItemClickEventArgs(hti.SubItem, hti.Item))
        End If
    End Sub
    Protected Overrides Sub OnMouseDoubleClick(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseDoubleClick(e)
        Dim hti As ListViewHitTestInfo = Me.HitTest(e.Location)
        If hti.Item.SubItems.IndexOf(hti.SubItem) > 0 Then
            RaiseEvent SubItemDoubleClick(Me, New SubItemClickEventArgs(hti.SubItem, hti.Item))
        End If
    End Sub

    Private Sub cListView_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.HandleCreated
        SetBkImage()
        ShowHeaderIcon(Me, ColumnSortIndex, lvwColumnSorter.OrderOfSort)
    End Sub

    Protected Overrides Sub OnHandleCreated(ByVal e As System.EventArgs)
        MyBase.OnHandleCreated(e)
        If IsVistaOrLater And UseVistaThemeing Then
            SetWindowTheme(Handle, "explorer", Nothing)
        End If
    End Sub


    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        MyBase.WndProc(m)
        If m.Msg = &H18 Then
            ShowHeaderIcon(Me, ColumnSortIndex, lvwColumnSorter.OrderOfSort)
            Invalidate()
        End If
    End Sub

    'Private Sub cListView_Invalidated(sender As Object, e As System.Windows.Forms.InvalidateEventArgs) Handles Me.Invalidated
    '    For Each item As ListViewItem In Items
    '        If item Is Nothing Then Return
    '        item.Tag = Nothing
    '    Next
    'End Sub

    'Private Sub cListView_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
    '    Dim item As ListViewItem = Me.GetItemAt(e.X, e.Y)
    '    If item IsNot Nothing AndAlso item.Tag Is Nothing Then
    '        Invalidate(item.Bounds)
    '        item.Tag = "tagged"
    '    End If
    '    Invalidate()
    'End Sub
End Class



Namespace SvenSo
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '''' CREDITS TO
    '''' Sven So
    '''' http://www.codeproject.com/Articles/5332/ListView-Column-Sorter
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    ''' <summary>
    ''' This class is an implementation of the 'IComparer' interface.
    ''' </summary>
    Public Class ListViewColumnSorter
        Implements IComparer
        Public Enum SortModifiers
            SortByText = 1
            SortByImage = 2
            SortByCheckbox = 3
        End Enum

        ''' <summary>
        ''' Specifies the column to be sorted
        ''' </summary>
        Public ColumnToSort As Integer
        ''' <summary>
        ''' Specifies the order in which to sort (i.e. 'Ascending').
        ''' </summary>
        Public OrderOfSort As SortOrder
        ''' <summary>
        ''' Case insensitive comparer object
        ''' </summary>

        Private ObjectCompare As NumberCaseInsensitiveComparer
        Private FirstObjectCompare As ImageTextComparer
        Private FirstObjectCompare2 As CheckboxTextComparer

        Private mySortModifier As SortModifiers = SortModifiers.SortByText
        Public Property _SortModifier() As SortModifiers
            Get
                Return mySortModifier
            End Get
            Set(ByVal value As SortModifiers)
                mySortModifier = value
            End Set
        End Property

        ''' <summary>
        ''' Class constructor.  Initializes various elements
        ''' </summary>
        Public Sub New()
            ' Initialize the column to '0'
            ColumnToSort = 0
            OrderOfSort = SortOrder.Ascending
            ' Initialize the CaseInsensitiveComparer object
            ObjectCompare = New NumberCaseInsensitiveComparer()
            FirstObjectCompare = New ImageTextComparer()
            FirstObjectCompare2 = New CheckboxTextComparer()
        End Sub

        ''' <summary>
        ''' This method is inherited from the IComparer interface.  It compares the two objects passed using a case insensitive comparison.
        ''' </summary>
        ''' <param name="x">First object to be compared</param>
        ''' <param name="y">Second object to be compared</param>
        ''' <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
            Dim compareResult As Integer = 0
            Dim listviewX As ListViewItem, listviewY As ListViewItem

            ' Cast the objects to be compared to ListViewItem objects
            listviewX = DirectCast(x, ListViewItem)
            listviewY = DirectCast(y, ListViewItem)

            Dim listViewMain As ListView = listviewX.ListView

            ' Calculate correct return value based on object comparison
            If listViewMain.Sorting <> SortOrder.Ascending AndAlso listViewMain.Sorting <> SortOrder.Descending Then
                ' Return '0' to indicate they are equal
                Return compareResult
            End If

            If mySortModifier.Equals(SortModifiers.SortByText) OrElse ColumnToSort > 0 Then
                ' Compare the two items

                If listviewX.SubItems.Count <= ColumnToSort AndAlso listviewY.SubItems.Count <= ColumnToSort Then
                    compareResult = ObjectCompare.Compare(Nothing, Nothing)
                ElseIf listviewX.SubItems.Count <= ColumnToSort AndAlso listviewY.SubItems.Count > ColumnToSort Then
                    compareResult = ObjectCompare.Compare(Nothing, listviewY.SubItems(ColumnToSort).Text.Trim())
                ElseIf listviewX.SubItems.Count > ColumnToSort AndAlso listviewY.SubItems.Count <= ColumnToSort Then
                    compareResult = ObjectCompare.Compare(listviewX.SubItems(ColumnToSort).Text.Trim(), Nothing)
                Else
                    compareResult = ObjectCompare.Compare(listviewX.SubItems(ColumnToSort).Text.Trim(), listviewY.SubItems(ColumnToSort).Text.Trim())
                End If
            Else
                Select Case mySortModifier
                    Case SortModifiers.SortByCheckbox
                        compareResult = FirstObjectCompare2.Compare(x, y)
                        Exit Select
                    Case SortModifiers.SortByImage
                        compareResult = FirstObjectCompare.Compare(x, y)
                        Exit Select
                    Case Else
                        compareResult = FirstObjectCompare.Compare(x, y)
                        Exit Select
                End Select
            End If

            ' Calculate correct return value based on object comparison
            If OrderOfSort = SortOrder.Ascending Then
                ' Ascending sort is selected, return normal result of compare operation
                Return compareResult
            ElseIf OrderOfSort = SortOrder.Descending Then
                ' Descending sort is selected, return negative result of compare operation
                Return (-compareResult)
            Else
                ' Return '0' to indicate they are equal
                Return 0
            End If
        End Function

        ''' <summary>
        ''' Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
        ''' </summary>
        Public Property SortColumn() As Integer
            Get
                Return ColumnToSort
            End Get
            Set(ByVal value As Integer)
                ColumnToSort = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
        ''' </summary>
        Public Property Order() As SortOrder
            Get
                Return OrderOfSort
            End Get
            Set(ByVal value As SortOrder)
                OrderOfSort = value
            End Set
        End Property

    End Class

    Public Class ImageTextComparer
        Implements IComparer
        'private CaseInsensitiveComparer ObjectCompare;
        Private ObjectCompare As NumberCaseInsensitiveComparer

        Public Sub New()
            ' Initialize the CaseInsensitiveComparer object
            ObjectCompare = New NumberCaseInsensitiveComparer()
        End Sub

        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
            'int compareResult;
            Dim image1 As Integer, image2 As Integer
            Dim listviewX As ListViewItem, listviewY As ListViewItem

            ' Cast the objects to be compared to ListViewItem objects
            listviewX = DirectCast(x, ListViewItem)
            image1 = listviewX.ImageIndex
            listviewY = DirectCast(y, ListViewItem)
            image2 = listviewY.ImageIndex

            If image1 < image2 Then
                Return -1
            ElseIf image1 = image2 Then
                Return ObjectCompare.Compare(listviewX.Text.Trim(), listviewY.Text.Trim())
            Else
                Return 1
            End If
        End Function
    End Class

    Public Class CheckboxTextComparer
        Implements IComparer
        Private ObjectCompare As NumberCaseInsensitiveComparer

        Public Sub New()
            ' Initialize the CaseInsensitiveComparer object
            ObjectCompare = New NumberCaseInsensitiveComparer()
        End Sub

        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
            ' Cast the objects to be compared to ListViewItem objects
            Dim listviewX As ListViewItem = DirectCast(x, ListViewItem)
            Dim listviewY As ListViewItem = DirectCast(y, ListViewItem)

            If listviewX.Checked AndAlso Not listviewY.Checked Then
                Return -1
            ElseIf listviewX.Checked.Equals(listviewY.Checked) Then
                If listviewX.ImageIndex < listviewY.ImageIndex Then
                    Return -1
                ElseIf listviewX.ImageIndex = listviewY.ImageIndex Then
                    Return ObjectCompare.Compare(listviewX.Text.Trim(), listviewY.Text.Trim())
                Else
                    Return 1
                End If
            Else
                Return 1
            End If
        End Function
    End Class


    Public Class NumberCaseInsensitiveComparer
        Inherits CaseInsensitiveComparer

        Public Sub New()
        End Sub

        Public Shadows Function Compare(ByVal x As Object, ByVal y As Object) As Integer
            If x Is Nothing AndAlso y Is Nothing Then
                Return 0
            ElseIf x Is Nothing AndAlso y IsNot Nothing Then
                Return -1
            ElseIf x IsNot Nothing AndAlso y Is Nothing Then
                Return 1
            End If
            If (TypeOf x Is System.String) AndAlso IsWholeNumber(DirectCast(x, String)) AndAlso (TypeOf y Is System.String) AndAlso IsWholeNumber(DirectCast(y, String)) Then
                Try
                    Return MyBase.Compare(Convert.ToUInt64(DirectCast(x, String).Trim()), Convert.ToUInt64(DirectCast(y, String).Trim()))
                Catch
                    Return -1
                End Try
            Else
                Return MyBase.Compare(x, y)
            End If
        End Function

        Private Function IsWholeNumber(ByVal strNumber As String) As Boolean
            Dim wholePattern As New System.Text.RegularExpressions.Regex("^\d+$")
            Return wholePattern.IsMatch(strNumber)
        End Function
    End Class

End Namespace

Public Class SubItemClickEventArgs
    Inherits EventArgs
    Public SubItem As ListViewItem.ListViewSubItem
    Public Item As ListViewItem
    Public Sub New(ByVal Sub_Item As ListViewItem.ListViewSubItem, ByVal Item_ As ListViewItem)
        SubItem = Sub_Item
        Item = Item_
    End Sub
End Class