﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System
Imports System.Runtime.Serialization

Namespace DoMucServiceReference
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0"),  _
     System.Runtime.Serialization.DataContractAttribute(Name:="DoMuc", [Namespace]:="http://schemas.datacontract.org/2004/07/Entity"),  _
     System.SerializableAttribute()>  _
    Partial Public Class DoMuc
        Inherits Object
        Implements System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged
        
        <System.NonSerializedAttribute()>  _
        Private extensionDataField As System.Runtime.Serialization.ExtensionDataObject
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private AvgTempField As Double
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private DateUpdateField As Date
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private FlowRateField As Double
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private IDField As Integer
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private LevelField As Double
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private LevelRateField As Double
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private MaBeField As String
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private MaHangHoaField As String
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private TOVField As Double
        
        <Global.System.ComponentModel.BrowsableAttribute(false)>  _
        Public Property ExtensionData() As System.Runtime.Serialization.ExtensionDataObject Implements System.Runtime.Serialization.IExtensibleDataObject.ExtensionData
            Get
                Return Me.extensionDataField
            End Get
            Set
                Me.extensionDataField = value
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property AvgTemp() As Double
            Get
                Return Me.AvgTempField
            End Get
            Set
                If (Me.AvgTempField.Equals(value) <> true) Then
                    Me.AvgTempField = value
                    Me.RaisePropertyChanged("AvgTemp")
                End If
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property DateUpdate() As Date
            Get
                Return Me.DateUpdateField
            End Get
            Set
                If (Me.DateUpdateField.Equals(value) <> true) Then
                    Me.DateUpdateField = value
                    Me.RaisePropertyChanged("DateUpdate")
                End If
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property FlowRate() As Double
            Get
                Return Me.FlowRateField
            End Get
            Set
                If (Me.FlowRateField.Equals(value) <> true) Then
                    Me.FlowRateField = value
                    Me.RaisePropertyChanged("FlowRate")
                End If
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property ID() As Integer
            Get
                Return Me.IDField
            End Get
            Set
                If (Me.IDField.Equals(value) <> true) Then
                    Me.IDField = value
                    Me.RaisePropertyChanged("ID")
                End If
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property Level() As Double
            Get
                Return Me.LevelField
            End Get
            Set
                If (Me.LevelField.Equals(value) <> true) Then
                    Me.LevelField = value
                    Me.RaisePropertyChanged("Level")
                End If
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property LevelRate() As Double
            Get
                Return Me.LevelRateField
            End Get
            Set
                If (Me.LevelRateField.Equals(value) <> true) Then
                    Me.LevelRateField = value
                    Me.RaisePropertyChanged("LevelRate")
                End If
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property MaBe() As String
            Get
                Return Me.MaBeField
            End Get
            Set
                If (Object.ReferenceEquals(Me.MaBeField, value) <> true) Then
                    Me.MaBeField = value
                    Me.RaisePropertyChanged("MaBe")
                End If
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property MaHangHoa() As String
            Get
                Return Me.MaHangHoaField
            End Get
            Set
                If (Object.ReferenceEquals(Me.MaHangHoaField, value) <> true) Then
                    Me.MaHangHoaField = value
                    Me.RaisePropertyChanged("MaHangHoa")
                End If
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property TOV() As Double
            Get
                Return Me.TOVField
            End Get
            Set
                If (Me.TOVField.Equals(value) <> true) Then
                    Me.TOVField = value
                    Me.RaisePropertyChanged("TOV")
                End If
            End Set
        End Property
        
        Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
        
        Protected Sub RaisePropertyChanged(ByVal propertyName As String)
            Dim propertyChanged As System.ComponentModel.PropertyChangedEventHandler = Me.PropertyChangedEvent
            If (Not (propertyChanged) Is Nothing) Then
                propertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
            End If
        End Sub
    End Class
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ServiceModel.ServiceContractAttribute([Namespace]:="", ConfigurationName:="DoMucServiceReference.DoMucServices")>  _
    Public Interface DoMucServices
        
        <System.ServiceModel.OperationContractAttribute(Action:="urn:DoMucServices/DoWork", ReplyAction:="urn:DoMucServices/DoWorkResponse")>  _
        Sub DoWork()
        
        <System.ServiceModel.OperationContractAttribute(Action:="urn:DoMucServices/Hello", ReplyAction:="urn:DoMucServices/HelloResponse")>  _
        Function Hello() As String
        
        <System.ServiceModel.OperationContractAttribute(Action:="urn:DoMucServices/HelloIm", ReplyAction:="urn:DoMucServices/HelloImResponse")>  _
        Function HelloIm(ByVal name As String) As String
        
        <System.ServiceModel.OperationContractAttribute(Action:="urn:DoMucServices/LayThongTinDoMucBe", ReplyAction:="urn:DoMucServices/LayThongTinDoMucBeResponse")>  _
        Function LayThongTinDoMucBe(ByVal be As String) As DoMucServiceReference.DoMuc
        
        <System.ServiceModel.OperationContractAttribute(Action:="urn:DoMucServices/LayThongTinDoMucKho", ReplyAction:="urn:DoMucServices/LayThongTinDoMucKhoResponse")>  _
        Function LayThongTinDoMucKho(ByVal kho As String) As DoMucServiceReference.DoMuc()
        
        <System.ServiceModel.OperationContractAttribute(Action:="urn:DoMucServices/SendDataDoMuc", ReplyAction:="urn:DoMucServices/SendDataDoMucResponse")>  _
        Function SendDataDoMuc(ByVal TankName As String, ByVal Level As Double, ByVal LevelRate As Double, ByVal AvgTemp As Double, ByVal FlowRate As Double, ByVal TOV As Double) As String
    End Interface
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Public Interface DoMucServicesChannel
        Inherits DoMucServiceReference.DoMucServices, System.ServiceModel.IClientChannel
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Partial Public Class DoMucServicesClient
        Inherits System.ServiceModel.ClientBase(Of DoMucServiceReference.DoMucServices)
        Implements DoMucServiceReference.DoMucServices
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String)
            MyBase.New(endpointConfigurationName)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As String)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(binding, remoteAddress)
        End Sub
        
        Public Sub DoWork() Implements DoMucServiceReference.DoMucServices.DoWork
            MyBase.Channel.DoWork
        End Sub
        
        Public Function Hello() As String Implements DoMucServiceReference.DoMucServices.Hello
            Return MyBase.Channel.Hello
        End Function
        
        Public Function HelloIm(ByVal name As String) As String Implements DoMucServiceReference.DoMucServices.HelloIm
            Return MyBase.Channel.HelloIm(name)
        End Function
        
        Public Function LayThongTinDoMucBe(ByVal be As String) As DoMucServiceReference.DoMuc Implements DoMucServiceReference.DoMucServices.LayThongTinDoMucBe
            Return MyBase.Channel.LayThongTinDoMucBe(be)
        End Function
        
        Public Function LayThongTinDoMucKho(ByVal kho As String) As DoMucServiceReference.DoMuc() Implements DoMucServiceReference.DoMucServices.LayThongTinDoMucKho
            Return MyBase.Channel.LayThongTinDoMucKho(kho)
        End Function
        
        Public Function SendDataDoMuc(ByVal TankName As String, ByVal Level As Double, ByVal LevelRate As Double, ByVal AvgTemp As Double, ByVal FlowRate As Double, ByVal TOV As Double) As String Implements DoMucServiceReference.DoMucServices.SendDataDoMuc
            Return MyBase.Channel.SendDataDoMuc(TankName, Level, LevelRate, AvgTemp, FlowRate, TOV)
        End Function
    End Class
End Namespace
