rem -*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-
rem --- Service Registration --- 
rem -*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-

@echo off
setlocal
set _CURPATH=%~dp0

rem HISRISDownloadService
SC Create StaffLinkage binPath= "%_CURPATH%\StaffLinkage.exe" start= delayed-auto DisplayName= "けいゆう病院_職員マスタ連携サービス"
echo サービス登録 けいゆう病院_職員マスタ連携サービス(StaffLinkage)
rem SC Query HISRISDownloadService

rem HISRISDownloadPatientService
rem SC Create HISRISDownloadPatientService binPath= "%_CURPATH%DownloadPatientServ\DownloadPatientServ.exe" start= delayed-auto DisplayName= "HIS-RIS患者ダウンロードサービス"
rem echo サービス登録 HIS-RIS患者ダウンロードサービス(HISRISDownloadPatientService)
rem SC Query HISRISDownloadPatientService

rem ARISSendCost
rem SC Create ARISSendCostService binPath= "%_CURPATH%ARISSendCost\ARISSendCost.exe" start= delayed-auto DisplayName= "RIS-HIS実施送信_OGK"
rem echo サービス登録 RIS-HIS実施送信_OGK(ARISReceiveOrderService)
rem SC Query ARISSendCostService

rem ARISSendPatient
rem SC Create ARISSendPatientService binPath= "%_CURPATH%ARISSendPatient\ARISSendPatient.exe" start= delayed-auto DisplayName= "RIS-HIS患者要求送信_OGK"
rem echo サービス登録 RIS-HIS患者要求送信_OGK(ARISReceiveOrderService)
rem SC Query ARISSendPatientService

rem ARISSendReceipt
rem SC Create ARISSendReceiptService binPath= "%_CURPATH%ARISSendReceipt\ARISSendReceipt.exe" start= delayed-auto DisplayName= "RIS-HIS受付送信_OGK"
rem echo サービス登録 RIS-HIS受付送信_OGK(ARISReceiveOrderService)
rem SC Query ARISSendReceiptService

rem ReportInterface
rem SC Create ReportInterfaceService binPath= "%_CURPATH%ReportInterface\ReportInterface.exe" start= delayed-auto DisplayName= "RIS-REPORT連携機能_OGK"
rem echo サービス登録 RIS-REPORT連携機能_OGK(ARISReceiveOrderService)
rem SC Query ReportInterfaceService

rem ExaminationResult
rem SC Create ExaminationResult binPath= "%_CURPATH%ExaminationResult\ExaminationResult.exe" start= delayed-auto DisplayName= "RIS-検査結果DWH参照機能_OGK"
rem echo サービス登録 RIS-検査結果DWH参照機能_OGK(ExaminationResult)
rem SC Query ExaminationResultService

rem StaffMaster
rem SC Create StaffMaster binPath= "%_CURPATH%StaffMaster\StaffMaster.exe" start= delayed-auto DisplayName= "RIS-職員マスタDWH参照機能_OGK"
rem echo サービス登録 RIS-職員マスタDWH参照機能_OGK(StaffMaster)
rem SC Query StaffMasterService

cmd /k
