import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../services/employee.service';
import { Employee } from '../models/employee.model';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css'],
  standalone: true,
  imports: [CommonModule, MatToolbarModule, MatTableModule, MatCardModule, MatPaginatorModule, MatFormFieldModule, MatInputModule, FormsModule]
})
export class EmployeeListComponent implements OnInit {
  employees: Employee[] = [];
  displayedColumns: string[] = ['firstName', 'lastName', 'dateOfJoining', 'lastSalary', 'employeeId'];
  searchTerm = '';
  pageNumber: number = 1;
  pageSize: number = 30;
  totalCount: number = 0;

  constructor(private employeeService: EmployeeService) { }

  ngOnInit(): void {
    
    this.getEmployees(this.pageNumber, this.pageSize);
  }

  getEmployees(pageNumber: number, pageSize: number): void {
    this.employeeService.getEmployees(pageNumber, pageSize)
      .subscribe((response: any) => {
        this.employees = response.data;
        this.totalCount = response.totalCount;
        this.pageNumber = response.pageNumber;
        this.pageSize = response.pageSize;
      });
  }
  searchEmployees() {
    this.employeeService.searchEmployees(this.pageNumber, this.pageSize, this.searchTerm)
      .subscribe((response: any) => {
        this.employees = response.data;
        this.totalCount = response.totalCount;
        this.pageNumber = response.pageNumber;
        this.pageSize = response.pageSize;
      });
    }
  onPageChange(event: PageEvent): void {
    this.pageNumber = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.getEmployees(this.pageNumber, this.pageSize);
  }
}
