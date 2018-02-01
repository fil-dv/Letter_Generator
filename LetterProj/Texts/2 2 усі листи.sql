delete from IMP_LET t;

insert into IMP_LET  (Proj, Addr, Inn , Tem )

  select
  
  p.id,    
  a.zip_code || '; ' || a.region || '; ' || a.city || '; ' || a.street,    
  nvl(c.inn, p.id) , --
  
  4232 ---- � 
  
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
  
   and a.role = 1 --!!  1 ��  2 ��  3 ��  4 ��
  
   and p.archive_flag = 0
  
  and currencypack.ConvertCurrency(p.debt_rest, sysdate, p.valute) >= 300 ----- !!!
  
  --      and nvl(p.ext_status10,0) = 0 -- ��� ����
  
 --  and p.paid_really = 0 -- ����
  
   and p.status_negativ = 0 -- ���
  
  /* and p.plan_id not in ( --3244, -- ���.�������� (����) 
                     3224 --  ���.���������������(���)
                     --                     3145, --� - ������ - ���
                     --                   3164 -- � - ������ - ���
                     )*/
  
  and p.last_result_id <> 3020 --  ���������� ��� ���� �������
  
  --   and p.group_id <> 1540 -- trash      
  
 --  and p.paid_need_confirm = 0 -- ��
  
   and not
    (lower(a.region) like '%�������%' or lower(a.region) like '%������%' or
    lower(a.region) like '%��������%' or lower(a.region) like '%�������%')
  
   and not (lower(a.city) like '%�������%' or lower(a.city) like '%������%' or
    lower(a.city) like '%��������%' or lower(a.city) like '%�������%')
  
   and not (lower(a.region) like '%����%' or lower(a.region) like '%����%' or
    lower(a.region) like '%������%')
  
   and not (lower(a.city) like '%�������%' or lower(a.city) like '%������%')
  
   and exists (select 1
      from suvd.todo_items ti
     where ti.project_id = p.id
       and ti.result_id = 1236) -- ����� �������� (����)
  
   and not exists (select 1
      from suvd.todo_items ti
     where ti.project_id = p.id
       and ti.result_id = 2559 -- �� ������        -- 2560 ��
    )
  
   and not exists (select 1
      from suvd.todo_items ti
     where ti.project_id = p.id
       and instr(ti.comments, '��-��������') > 0 -- ��-��������
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
