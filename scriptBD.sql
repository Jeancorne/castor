delete from users 
select * from users
select * from jobs
select * from employees 

-- ================================================================
-- ============= Registrar maestra de job ==============================

drop procedure prc_insertJob

call prc_insertJob('PO')

CREATE PROCEDURE prc_insertJob(    
    IN pName LONGTEXT    
)
BEGIN    
    IF EXISTS (SELECT 1 FROM jobs WHERE Name = pName) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'El Nombre ya existe en la tabla.';
    ELSE        
        INSERT INTO jobs (Id, Name, CreatedAt)
        VALUES (UUID(), pName, NOW());
    END IF;
END



-- ===========================================================
-- ============= CREACIÓN DE LAS TABLAS ======================

create table services(
  Id char(36) primary key not null,
  Name longtext not null,
  CreatedAt datetime(6) default null,
  UpdatedAt datetime(6) default null
)

create table stateApplicant(
  Id char(36) primary key not null,
  Name longtext not null,
  CreatedAt datetime(6) default null,
  UpdatedAt datetime(6) default null
)


create table applicant(
  Id char(36) primary key not null,
  IdServices char(36) not null,
  IdState char(36) not null,
  CreatedAt datetime(6) default null,
  UpdatedAt datetime(6) default null,
  NumRequest varchar(15) not null,
  IdUser char(36) CHARACTER SET ascii not null
)

-- ===========================================================
-- ============= CREACIÓN DE LAS RELACIONES ==================

ALTER TABLE applicant
ADD CONSTRAINT fk_IdServices
FOREIGN KEY (IdServices)
REFERENCES services (Id)

ALTER TABLE applicant
ADD CONSTRAINT fk_IdState
FOREIGN KEY (IdState)
REFERENCES stateApplicant (Id)


ALTER TABLE applicant
ADD CONSTRAINT fk_IdUser
FOREIGN KEY (IdUser)
REFERENCES users(Id)

-- ===========================================================
-- ============= INGRESAR DATOS ==============================


insert into services(Id, Name, CreatedAt)values(UUID(), 'Soporte', NOW())
insert into services(Id, Name, CreatedAt)values(UUID(), 'Análisis', NOW())


insert into stateApplicant(Id, Name, CreatedAt)values(UUID(), 'Activo', NOW())
insert into stateApplicant(Id, Name, CreatedAt)values(UUID(), 'Inactivo', NOW())


select * from services
select * from stateapplicant 
select * from users

insert into applicant(Id, IdServices, IdState, CreatedAt, NumRequest, IdUser)
values(
UUID(), 
'3f7f0453-5136-11ef-afa2-e8b5d0fa5aec',
'764748c5-5136-11ef-afa2-e8b5d0fa5aec',
NOW(),
'SOL-001',
'04afefea-99ce-4143-8cc9-63033e69fbe9'
)

-- ================================================================
-- ============= CREAR PROCEDIMIENTO ==============================


call prc_getListApplicant

CREATE PROCEDURE prc_getListApplicant()
BEGIN    
    select
    services.Name as NameServices,
    stateapplicant.Name as NameState,
    users.Name as NameUser,
    users.LastName as LastNameUser,
    applicant.*    
    from applicant
    inner join services on applicant.IdServices = services.Id
    inner join stateapplicant on applicant.IdState = stateapplicant.Id
    inner join users on applicant.IdUser  = users.Id;
END






