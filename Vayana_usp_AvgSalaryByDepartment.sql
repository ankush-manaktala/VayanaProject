Create Procedure usp_AvgSalaryByDepartment
As
Begin

Select AVG(Emp_Salary) as AvgSalary
	  ,d.Dept_Id As DeptId
	  ,d.Dept_Name AS Department
	   From Employee e
	   INNEr JOIN Department d
		On e.Emp_DeptId=d.Dept_Id ANd e.Emp_IsDeleted=0
		Group By d.Dept_Id 
		,d.Dept_Name

End