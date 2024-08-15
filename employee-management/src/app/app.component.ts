import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { EmployeeService } from './services/employee.service';
import { Employee } from './models/employee.model';
import { EmployeeListComponent } from './employee-list/employee-list.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  standalone: true,
  imports: [CommonModule, EmployeeListComponent]
})
export class AppComponent implements OnInit {
  employees: Employee[] = [];

 
  constructor(private http: HttpClient, private employeeService: EmployeeService) {}

  ngOnInit() {
    this.getEmployees();
  }

  getEmployees() {
    this.employeeService.getEmployees()
      .subscribe(
        data => {this.employees = data},
        error => console.error('Error fetching employees', error)
      );
  }
}

