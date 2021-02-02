--Assignment 1Feb

--sp to insert a row
create proc sp_InsertEmployee
@empname varchar(20),
@esal float,
@deptId int
as
begin
Insert into EmployeeTab values(@empname, @esal,@deptId)
end

execute sp_InsertEmployee Rahul,4232,12

--sp to delete a row
create proc sp_DeleteEmployee
@empid int
as
begin
delete from EmployeeTab where EmpId=@empid
end

execute sp_DeleteEmployee 29

select * from EmployeeTab

--sp to update values based on EmpId entered
create proc sp_UpdateEmp
@empid int,
@empname varchar(20),
@esal float,
@deptId int
as
begin
update EmployeeTab set EmpName=@empname, Salary =@esal,DeptNo=@deptId
where EmpId=@empid
end

execute sp_UpdateEmp 14,'Rajat',1434.34,11

select * from EmployeeTab

--sp to display employee details
create proc sp_ShowEmpDetails
@empid int
as
begin
select et.EmpId,et.EmpName,et.Salary, dt.DeptName
from EmployeeTab et
join DeptTab dt
on et.DeptNo = dt.DeptId
where EmpId =@empid
end
	
execute sp_ShowEmpDetails 17