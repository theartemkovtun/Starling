create table public.document (
	id uuid default uuid_generate_v4 (),
	name varchar not null,
	created_on timestamp default current_timestamp
);

alter table public.document add constraint id_pk primary key (id);

create table public.document_content (
	id uuid default uuid_generate_v4 (),
	document_id uuid not null,
	version integer not null,
	content bytea not null,
	created_on timestamp default current_timestamp
);

alter table public.document_content add constraint document_id_fk foreign key (document_id) REFERENCES  document(id);












