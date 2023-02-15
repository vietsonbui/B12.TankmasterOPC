Imports System.Configuration
Imports Newtonsoft.Json
Imports OPCAutomation

Module B12TankmasterOPC

    Public MyOPCServer As OPCServer = New OPCServer()
    Public My_OPCGroup As OPCGroup
    Private MyOPCItem As OPCItem
    Private MyServerHandles As Integer()
    'Private MyValues As Object() = New Object(1) {}
    Public MyErrors As Array

    Private _progID As String '= "SaabTankRadar.TankServer.1"
    Private _server As String '= "127.0.0.1"
    Private _updateRate As Integer = 1000
    Private _sendDBInterval As Integer = 10000
    Private _tankList As String = ""
    'Private _configFileName As String = "config.ini"

    Private dictClientHandle As New Dictionary(Of Integer, String)
    Private dictTank As New Dictionary(Of String, Tank)
    Public iClientHandleCount As Integer = 0

    Dim service As New DoMucServiceReference.DoMucServicesClient

    'Hide windows
    Private Declare Auto Function ShowWindow Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal nCmdShow As Integer) As Boolean
    Private Declare Auto Function GetConsoleWindow Lib "kernel32.dll" () As IntPtr
    Private SW_HIDE As Integer = 0

    '<DllImport("kernel32")>
    'Private Function GetPrivateProfileString(ByVal section As String, ByVal key As String, ByVal def As String, ByVal retVal As StringBuilder, ByVal size As Integer, ByVal filePath As String) As Integer
    'End Function

    Structure Tank
        Dim TankName As String
        Dim Level As Double
        Dim LevelRate As Double
        Dim AvgTemp As Double
        Dim FlowRate As Double
        Dim TOV As Double
    End Structure

    Public Sub Main()

        ReadConfig()

        Connect()

        AddItem()

        HideConsole()

        While True
            ReadValue()
            Threading.Thread.Sleep(_sendDBInterval)
        End While

        Disconnect()
    End Sub

    Private Sub HideConsole()
        Dim hWndConsole As IntPtr
        hWndConsole = GetConsoleWindow()
        ShowWindow(hWndConsole, SW_HIDE)
    End Sub

    Private Function SendData(Optional TankName As String = "TEST",
                         Optional Level As Double = 0,
                         Optional LevelRateLevel As Double = 0,
                         Optional AvgTempLevel As Double = 0,
                         Optional FlowRateLevel As Double = 0,
                         Optional TOVLevel As Double = 0) As String
        Return service.SendDataDoMuc(TankName, Level, LevelRateLevel, AvgTempLevel, FlowRateLevel, TOVLevel)
    End Function

    Private Sub Connect()
        Try
            Console.WriteLine(String.Format("Khoi tao ket noi {0} : {1}", _server, _progID))
            MyOPCServer.Connect(_progID, _server)
            My_OPCGroup = MyOPCServer.OPCGroups.Add("B12OPCGroup")
            My_OPCGroup.IsActive = True 'alway refreshed with the latest value
            My_OPCGroup.IsSubscribed = True 'subscribed to data change events
            My_OPCGroup.UpdateRate = _updateRate    '1 second

            'My_OPCGroup.DataChange += New DIOPCGroupEvent_DataChangeEventHandler(AddressOf My_OPCGroup_DataChange)
            'My_OPCGroup.AsyncWriteComplete += New DIOPCGroupEvent_AsyncWriteCompleteEventHandler(AddressOf My_OPCGroup_AsyncWriteComplete)
            'My_OPCGroup.AsyncReadComplete += New DIOPCGroupEvent_AsyncReadCompleteEventHandler(AddressOf My_OPCGroup_AsyncReadComplete)

            AddHandler My_OPCGroup.DataChange, New DIOPCGroupEvent_DataChangeEventHandler(AddressOf My_OPCGroup_DataChange)
            AddHandler My_OPCGroup.AsyncReadComplete, New DIOPCGroupEvent_AsyncReadCompleteEventHandler(AddressOf My_OPCGroup_AsyncReadComplete)

            Console.WriteLine(String.Format("Ket noi thanh cong {0} : {1}", _server, _progID))
        Catch ex As Exception
            Console.WriteLine("Loi ket noi. " & vbCrLf & ex.Message)
        End Try
    End Sub

    Private Sub Disconnect()
        Try
            Console.WriteLine(String.Format("Ngat ket noi {0} : {1}", _server, _progID))
            My_OPCGroup.IsSubscribed = False
            My_OPCGroup.IsActive = False
            MyOPCServer.OPCGroups.RemoveAll()
        Catch ex As Exception
            Console.WriteLine("Loi " & vbCrLf & ex.Message)
        End Try
    End Sub

    'Private Sub AddItem()
    '    MyOPCItem = My_OPCGroup.OPCItems.AddItem("KAD-01.LL.CV", 1)
    '    MyServerHandles(1) = MyOPCItem.ServerHandle
    'End Sub

    Private Sub AddItem()
        Try
            MyServerHandles = New Integer(_tankList.Split(";").Count * 5) {}

            For Each sTankName As String In _tankList.Split(";")

                Console.WriteLine("AddItem " & sTankName)
                'Dim sTankName As String = "KAM-01"
                Dim sFieldLevelName As String = "LL" 'H chung
                Dim sFieldAvgTempName As String = "AT" 'Nhiet do trung binh
                Dim sFieldFlowRateName As String = "FR" 'Luu toc M3/h
                Dim sFieldLevelRateName As String = "LR"
                Dim sFieldTOVName As String = "TOV"
                'Dim sFieldAVRMName As String = "AVRM" 'Trong be

                'MyOPCItem = My_OPCGroup.OPCItems.AddItem("KAD-01.LL.CV", 1)
                'Dim iClientHandle As Integer = CInt(Math.Ceiling(Rnd() * 999999999)) + 1

                iClientHandleCount += 1
                MyServerHandles(iClientHandleCount) = My_OPCGroup.OPCItems.AddItem(String.Format("{0}.{1}.CV", sTankName, sFieldLevelName), iClientHandleCount).ServerHandle
                dictClientHandle.Add(iClientHandleCount, String.Format("{0}.{1}.CV", sTankName, sFieldLevelName))

                iClientHandleCount += 1
                MyServerHandles(iClientHandleCount) = My_OPCGroup.OPCItems.AddItem(String.Format("{0}.{1}.CV", sTankName, sFieldAvgTempName), iClientHandleCount).ServerHandle
                dictClientHandle.Add(iClientHandleCount, String.Format("{0}.{1}.CV", sTankName, sFieldAvgTempName))

                iClientHandleCount += 1
                MyServerHandles(iClientHandleCount) = My_OPCGroup.OPCItems.AddItem(String.Format("{0}.{1}.CV", sTankName, sFieldFlowRateName), iClientHandleCount).ServerHandle
                dictClientHandle.Add(iClientHandleCount, String.Format("{0}.{1}.CV", sTankName, sFieldFlowRateName))

                iClientHandleCount += 1
                MyServerHandles(iClientHandleCount) = My_OPCGroup.OPCItems.AddItem(String.Format("{0}.{1}.CV", sTankName, sFieldLevelRateName), iClientHandleCount).ServerHandle
                dictClientHandle.Add(iClientHandleCount, String.Format("{0}.{1}.CV", sTankName, sFieldLevelRateName))

                iClientHandleCount += 1
                MyServerHandles(iClientHandleCount) = My_OPCGroup.OPCItems.AddItem(String.Format("{0}.{1}.CV", sTankName, sFieldTOVName), iClientHandleCount).ServerHandle
                dictClientHandle.Add(iClientHandleCount, String.Format("{0}.{1}.CV", sTankName, sFieldTOVName))

                Dim oTank As New Tank
                oTank.TankName = sTankName

                dictTank.Add(sTankName, oTank)
            Next

            Console.WriteLine(JsonConvert.SerializeObject(dictClientHandle, Formatting.Indented))
            Console.WriteLine(JsonConvert.SerializeObject(dictTank, Formatting.Indented))

        Catch ex As Exception
            Console.WriteLine("Loi AddItem " & vbCrLf & ex.Message)
        End Try
    End Sub

    Private Sub ReadValue()
        Try
            Dim transID As Integer
            My_OPCGroup.AsyncRead(iClientHandleCount, MyServerHandles, MyErrors, Date.Now.Second, transID)

            'Dim Handle As Integer() = New Integer(10) {}
            'Dim Values As Object() = New Object(1) {}
            'Dim Errors As Object() = New Object(1) {}

            'My_OPCGroup.SyncRead(OPCDataSource.OPCDevice, 2, Handle, Values, Errors)

            'Console.WriteLine(JsonConvert.SerializeObject(Handle, Formatting.Indented))
            'GetItemValues(2, Handle, Values)
        Catch ex As Exception
            Console.WriteLine("Loi ReadValue " & vbCrLf & ex.Message)
        End Try
    End Sub

    Private Sub My_OPCGroup_DataChange(ByVal TransactionID As Integer, ByVal NumItems As Integer, ByRef ClientHandles As Array, ByRef ItemValues As Array, ByRef Qualities As Array, ByRef TimeStamps As Array)
        Console.WriteLine("--------------------------DataChange")
        GetItemValues(NumItems, ClientHandles, ItemValues)
        Console.WriteLine("------------------------------------")
    End Sub

    Private Sub My_OPCGroup_AsyncReadComplete(ByVal TransactionID As Integer, ByVal NumItems As Integer, ByRef ClientHandles As Array, ByRef ItemValues As Array, ByRef Qualities As Array, ByRef TimeStamps As Array, ByRef Errors As Array)
        Console.WriteLine("ReadComplete------------------------")
        GetItemValues(NumItems, ClientHandles, ItemValues)
        Console.WriteLine("------------------------------------")
    End Sub

    Private Sub GetItemValues(NumItems As Integer, ClientHandles As Array, ItemValues As Array)
        'Console.WriteLine(ItemValues.GetValue(0).ToString())
        Try
            For i = 1 To NumItems
                'Console.WriteLine(dictClientHandle.Item(ClientHandles(i)).ToString())
                'Console.WriteLine("ClientHandles: " & ClientHandles(i).ToString)
                'Console.WriteLine("ItemValues: " & ItemValues.GetValue(i).ToString())
                'Console.WriteLine("      <<<<<>>>>>>>      ")
                'Console.WriteLine("Qualities: " & Qualities.GetValue(i).ToString())
                'Console.WriteLine("TimeStamps: " & TimeStamps.GetValue(i).ToString())

                If Not String.IsNullOrEmpty(dictClientHandle.Item(ClientHandles(i)).ToString()) Then
                    Dim sTankName = dictClientHandle.Item(ClientHandles(i)).Split(".")(0)
                    Dim sTankField = dictClientHandle.Item(ClientHandles(i)).Split(".")(1)

                    '"LL" 'H chung
                    '"AT" 'Nhiet do trung binh
                    '"FR" 'Luu toc M3/h
                    '"LR"
                    '"TOV"

                    Select Case sTankField
                        Case "LL"
                            Dim oTank As Tank = dictTank(sTankName)
                            oTank.Level = CDbl(ItemValues.GetValue(i).ToString())
                            dictTank(sTankName) = oTank
                        Case "AT"
                            Dim oTank As Tank = dictTank(sTankName)
                            oTank.AvgTemp = CDbl(ItemValues.GetValue(i).ToString())
                            dictTank(sTankName) = oTank
                        Case "FR"
                            Dim oTank As Tank = dictTank(sTankName)
                            oTank.FlowRate = CDbl(ItemValues.GetValue(i).ToString())
                            dictTank(sTankName) = oTank
                        Case "LR"
                            Dim oTank As Tank = dictTank(sTankName)
                            oTank.LevelRate = CDbl(ItemValues.GetValue(i).ToString())
                            dictTank(sTankName) = oTank
                        Case "TOV"
                            Dim oTank As Tank = dictTank(sTankName)
                            oTank.TOV = CDbl(ItemValues.GetValue(i).ToString())
                            dictTank(sTankName) = oTank
                    End Select
                End If
            Next
            For Each oTank As Tank In dictTank.Values
                Console.WriteLine(String.Format("{0}--{1}--{2}--{3}--{4}--{5}", oTank.TankName.ToString(),
                                                                                oTank.Level.ToString(),
                                                                                oTank.LevelRate.ToString(),
                                                                                oTank.AvgTemp.ToString(),
                                                                                oTank.FlowRate.ToString(),
                                                                                oTank.TOV.ToString()))

                Console.WriteLine(SendData(oTank.TankName, oTank.Level, oTank.LevelRate, oTank.AvgTemp, oTank.FlowRate, oTank.TOV))
            Next
            'Console.WriteLine(JsonConvert.SerializeObject(dictTank, Formatting.Indented))
        Catch ex As Exception
            Console.WriteLine("Loi GetItemValues " & vbCrLf & ex.Message)
        End Try
    End Sub

    Private Sub My_OPCGroup_AsyncWriteComplete(ByVal TransactionID As Integer, ByVal NumItems As Integer, ByRef ClientHandles As Array, ByRef Errors As Array)
    End Sub

    'Public Function GetIniValue(section As String, key As String, filename As String, Optional defaultValue As String = "") As String
    '    Dim sb As New StringBuilder(500)
    '    If GetPrivateProfileString(section, key, defaultValue, sb, sb.Capacity, filename) > 0 Then
    '        Return sb.ToString
    '    Else
    '        Return defaultValue
    '    End If
    'End Function

    Private Sub ReadConfig()

        _server = ConfigurationManager.AppSettings("Server")
        _progID = ConfigurationManager.AppSettings("ProgID")
        Integer.TryParse(ConfigurationManager.AppSettings("SendDBInterval"), _sendDBInterval)
        Integer.TryParse(ConfigurationManager.AppSettings("UpdateRate"), _updateRate)
        _tankList = ConfigurationManager.AppSettings("TankList")
        Integer.TryParse(ConfigurationManager.AppSettings("SW_HIDE"), SW_HIDE)

        '_server = GetIniValue("Connect", "Server", String.Format("{0}/{1}", My.Application.Info.DirectoryPath, _configFileName))
        '_progID = GetIniValue("Connect", "ProgID", String.Format("{0}/{1}", My.Application.Info.DirectoryPath, _configFileName))
        '_sendDBInterval = CInt(GetIniValue("Connect", "SendDBInterval", String.Format("{0}/{1}", My.Application.Info.DirectoryPath, _configFileName)))
        '_updateRate = CInt(GetIniValue("Connect", "UpdateRate", String.Format("{0}/{1}", My.Application.Info.DirectoryPath, _configFileName)))
        '_tankList = GetIniValue("Tank", "TankList", String.Format("{0}/{1}", My.Application.Info.DirectoryPath, _configFileName))
    End Sub
End Module
