﻿Public Class mDataSmoothing

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        With Me.cbMethods
            .Items.Clear()
            .ValueMember = "Value"
            .DisplayMember = "Key"
            .Items.Add(New KeyValuePair(Of String, cNumericalMethods.SmoothingMethod)(My.Resources.SmoothingMethod_SavitzkyGolay, cNumericalMethods.SmoothingMethod.SavitzkyGolay))
            .Items.Add(New KeyValuePair(Of String, cNumericalMethods.SmoothingMethod)(My.Resources.SmoothingMethod_AdjacentAverage, cNumericalMethods.SmoothingMethod.AdjacentAverageSmooth))
            .SelectedIndex = 0
        End With
    End Sub

    ''' <summary>
    ''' Constructor
    ''' </summary>
    Private Sub mDataSmoothing_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
       
    End Sub

    ''' <summary>
    ''' Change the description of the Smoothing Method.
    ''' </summary>
    Private Sub cbMethods_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbMethods.SelectedIndexChanged
        If Me.cbMethods.SelectedItem Is Nothing Then Return
        
        ' Show Description of Smoothing Method:
        Me.txtDescription.Text = cNumericalMethods.GetSmoothingDescriptionFromType(Me.SelectedSmoothingMethod)

        ' Write Property-Name and set Maximum Values for the Smoothing Property:
        Select Case Me.SelectedSmoothingMethod
            Case cNumericalMethods.SmoothingMethod.AdjacentAverageSmooth
                Me.lblPropertyName.Text = My.Resources.SmoothingPropertyName_AdjacentAverage
                Me.udSmoothProperties.Maximum = 500
                Me.udSmoothProperties.Minimum = 1
                Me.udSmoothProperties.Value = 3
            Case cNumericalMethods.SmoothingMethod.SavitzkyGolay
                Me.lblPropertyName.Text = My.Resources.SmoothingPropertyName_SavitzkyGolay
                Me.udSmoothProperties.Maximum = 12
                Me.udSmoothProperties.Minimum = 2
                Me.udSmoothProperties.Value = 5
        End Select
    End Sub

    ''' <summary>
    ''' Set/Get selected Smoothing Method
    ''' </summary>
    Public Property SelectedSmoothingMethod() As cNumericalMethods.SmoothingMethod
        Get
            If Me.cbMethods.SelectedItem Is Nothing Then Return cNumericalMethods.SmoothingMethod.AdjacentAverageSmooth
            Return DirectCast(Me.cbMethods.SelectedItem, KeyValuePair(Of String, cNumericalMethods.SmoothingMethod)).Value
        End Get
        Set(value As cNumericalMethods.SmoothingMethod)
            For Each Method As KeyValuePair(Of String, cNumericalMethods.SmoothingMethod) In Me.cbMethods.Items
                If Method.Value = value Then
                    Me.cbMethods.SelectedItem = Method
                End If
            Next
        End Set
    End Property

    ''' <summary>
    ''' Set/Get selected NeighborNumber
    ''' </summary>
    Public Property SmoothingParameter() As Integer
        Get
            Return Convert.ToInt32(Me.udSmoothProperties.Value)
        End Get
        Set(value As Integer)
            Me.udSmoothProperties.Value = value
        End Set
    End Property


End Class