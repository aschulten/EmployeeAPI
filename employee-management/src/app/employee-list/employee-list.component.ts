import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../services/employee.service'; // Asegúrate de tener la ruta correcta
import { Employee } from '../models/employee.model'; // Importa la entidad Employee
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css'],
  standalone: true,
  imports: [CommonModule, MatTableModule, MatCardModule]
})
export class EmployeeListComponent implements OnInit {
  employees: Employee[] = [];
  displayedColumns: string[] = ['id', 'firstName', 'lastName', 'dateOfJoining', 'lastSalary'];

  constructor(private employeeService: EmployeeService) { }

  
  ngOnInit(): void {
    this.getEmployees();
  }

  getEmployees(): void {
    this.employeeService.getEmployees()
      .subscribe((data: Employee[]) => {
        this.employees = data.map(emp => ({
          id: emp.id,
          firstName: emp.firstName,
          lastName: emp.lastName,
          dateOfJoining: emp.dateOfJoining,
          lastSalary: emp.lastSalary,
          employeeId: emp.employeeId,
        }));
      });
  }
}