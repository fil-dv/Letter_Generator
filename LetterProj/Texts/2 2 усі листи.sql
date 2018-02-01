delete from IMP_LET t;

insert into IMP_LET  (Proj, Addr, Inn , Tem )

  select
  
  p.id,    
  a.zip_code || '; ' || a.region || '; ' || a.city || '; ' || a.street,    
  nvl(c.inn, p.id) , --
  
  4232 ---- ш 
  
  --count(*) --
  
    from suvd.projects p, suvd.contact_address a, suvd.contacts c
   where
  
   p.dogovor_id in (
30721
,
30722
,
30861
,
30862
                    ) --!!
  
   and a.contact_id = p.debtor_contact_id
   and c.id = p.debtor_contact_id
  
   and a.role = 1 --!!  1 АП  2 АТ  3 АФ  4 АР
  
   and p.archive_flag = 0
  
  and currencypack.ConvertCurrency(p.debt_rest, sysdate, p.valute) >= 300 ----- !!!
  
  --      and nvl(p.ext_status10,0) = 0 -- пот плат
  
 --  and p.paid_really = 0 -- плат
  
   and p.status_negativ = 0 -- нег
  
  /* and p.plan_id not in ( --3244, -- ТЕХ.Списание (ИПЧД) 
                     3224 --  ТЕХ.Бесперспективно(УКЦ)
                     --                     3145, --К - Юристы - Суд
                     --                   3164 -- К - Юристы - ГИС
                     )*/
  
  and p.last_result_id <> 3020 --  Утверждает что долг погашен
  
  --   and p.group_id <> 1540 -- trash      
  
 --  and p.paid_need_confirm = 0 -- ОП
  
   and not
    (lower(a.region) like '%донецьк%' or lower(a.region) like '%донецк%' or
    lower(a.region) like '%луганськ%' or lower(a.region) like '%луганск%')
  
   and not (lower(a.city) like '%донецьк%' or lower(a.city) like '%донецк%' or
    lower(a.city) like '%луганськ%' or lower(a.city) like '%луганск%')
  
   and not (lower(a.region) like '%крим%' or lower(a.region) like '%крым%' or
    lower(a.region) like '%еспубл%')
  
   and not (lower(a.city) like '%евастоп%' or lower(a.city) like '%мфероп%')
  
   and exists (select 1
      from suvd.todo_items ti
     where ti.project_id = p.id
       and ti.result_id = 1236) -- Адрес проверен (проп)
  
   and not exists (select 1
      from suvd.todo_items ti
     where ti.project_id = p.id
       and ti.result_id = 2559 -- АП пустой        -- 2560 АФ
    )
  
   and not exists (select 1
      from suvd.todo_items ti
     where ti.project_id = p.id
       and instr(ti.comments, 'АП-неверный') > 0 -- АФ-неверный
    )
  
   and length(a.zip_code || a.city) > 6
  
  --  and p.id in ( 1822859	, 1822947 )
  
  ---
/*  
   and not exists (select 1
      from suvd.letter_queue q
     where q.project_id = p.id and q.address_type = 1
       and q.template_id in (8491, 8671))*/
  
  ---
