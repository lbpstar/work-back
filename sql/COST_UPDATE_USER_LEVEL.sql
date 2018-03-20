create procedure COST_UPDATE_USER_LEVEL
as
update i set i.e_band = d.PERSON_LEVEL  from OPENQUERY (BARCODE, 'SELECT employee_id_,e_band FROM IHPS_ID_USER_PROFILE where department_ like ''制造中心%''') i join COST_DIRECT_LABOUR d on i.employee_id_ = d.CNO