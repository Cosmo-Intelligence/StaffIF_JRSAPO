rem -*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-
rem --- Service Registration Delete --- 
rem -*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-

@echo off
setlocal
set _CURPATH=%~dp0

rem SC Delete ARISReceiveHospitalService
SC Delete StaffLinkage
rem SC Delete ARISSendCostService
rem SC Delete HISRISDownloadPatientService
rem SC Delete ARISSendReceiptService
rem SC Delete ReportInterfaceService
rem SC Delete ExaminationResult
rem SC Delete StaffMaster

cmd /k
