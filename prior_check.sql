select p.business_n,
       s.priority_value 
  from SUVD.SCHEDULED_TODO_ITEMS s,
       SUVD.PROJECTS p
 where s.project_id = p.id
   and p.business_n in (select t.deal_id
                          from report.IMP_PRIOR t)
                     
