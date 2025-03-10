rem -*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-
rem --- Service Registration --- 
rem -*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-

@echo off
setlocal
set _CURPATH=%~dp0

rem HISRISDownloadService
SC Create StaffLinkage binPath= "%_CURPATH%\StaffLinkage.exe" start= delayed-auto DisplayName= "�����䂤�a�@_�E���}�X�^�A�g�T�[�r�X"
echo �T�[�r�X�o�^ �����䂤�a�@_�E���}�X�^�A�g�T�[�r�X(StaffLinkage)
rem SC Query HISRISDownloadService

rem HISRISDownloadPatientService
rem SC Create HISRISDownloadPatientService binPath= "%_CURPATH%DownloadPatientServ\DownloadPatientServ.exe" start= delayed-auto DisplayName= "HIS-RIS���҃_�E�����[�h�T�[�r�X"
rem echo �T�[�r�X�o�^ HIS-RIS���҃_�E�����[�h�T�[�r�X(HISRISDownloadPatientService)
rem SC Query HISRISDownloadPatientService

rem ARISSendCost
rem SC Create ARISSendCostService binPath= "%_CURPATH%ARISSendCost\ARISSendCost.exe" start= delayed-auto DisplayName= "RIS-HIS���{���M_OGK"
rem echo �T�[�r�X�o�^ RIS-HIS���{���M_OGK(ARISReceiveOrderService)
rem SC Query ARISSendCostService

rem ARISSendPatient
rem SC Create ARISSendPatientService binPath= "%_CURPATH%ARISSendPatient\ARISSendPatient.exe" start= delayed-auto DisplayName= "RIS-HIS���җv�����M_OGK"
rem echo �T�[�r�X�o�^ RIS-HIS���җv�����M_OGK(ARISReceiveOrderService)
rem SC Query ARISSendPatientService

rem ARISSendReceipt
rem SC Create ARISSendReceiptService binPath= "%_CURPATH%ARISSendReceipt\ARISSendReceipt.exe" start= delayed-auto DisplayName= "RIS-HIS��t���M_OGK"
rem echo �T�[�r�X�o�^ RIS-HIS��t���M_OGK(ARISReceiveOrderService)
rem SC Query ARISSendReceiptService

rem ReportInterface
rem SC Create ReportInterfaceService binPath= "%_CURPATH%ReportInterface\ReportInterface.exe" start= delayed-auto DisplayName= "RIS-REPORT�A�g�@�\_OGK"
rem echo �T�[�r�X�o�^ RIS-REPORT�A�g�@�\_OGK(ARISReceiveOrderService)
rem SC Query ReportInterfaceService

rem ExaminationResult
rem SC Create ExaminationResult binPath= "%_CURPATH%ExaminationResult\ExaminationResult.exe" start= delayed-auto DisplayName= "RIS-��������DWH�Q�Ƌ@�\_OGK"
rem echo �T�[�r�X�o�^ RIS-��������DWH�Q�Ƌ@�\_OGK(ExaminationResult)
rem SC Query ExaminationResultService

rem StaffMaster
rem SC Create StaffMaster binPath= "%_CURPATH%StaffMaster\StaffMaster.exe" start= delayed-auto DisplayName= "RIS-�E���}�X�^DWH�Q�Ƌ@�\_OGK"
rem echo �T�[�r�X�o�^ RIS-�E���}�X�^DWH�Q�Ƌ@�\_OGK(StaffMaster)
rem SC Query StaffMasterService

cmd /k
