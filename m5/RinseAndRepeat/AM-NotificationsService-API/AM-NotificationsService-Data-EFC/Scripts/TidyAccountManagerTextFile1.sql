select 'ALTER TABLE ' + p.name + ' DROP CONSTRAINT ' + t.name from sys.objects t
inner join sys.objects p on t.parent_object_id = p.object_id 
where t.name like 'FK%_Emails_%'
or t.name like 'FK%_SMSs_%'
or t.name like 'FK%_WebhookPosts_%'

Drop table Emails
Drop table SMSs
Drop table WebhookPosts