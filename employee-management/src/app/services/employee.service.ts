import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Employee } from '../models/employee.model';
import { PaginatedResponse } from '../models/paginatedResponse.model';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  
  private apiUrl = 'http://localhost:5124/api/employee';
  private apiKey = '1234567890abcdef';

  constructor(private http: HttpClient) { }

  getEmployees(pageNumber: number, pageSize: number): Observable<PaginatedResponse> {
    const headers = new HttpHeaders({
      'X-API-KEY': this.apiKey
    });
    const params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());
    return this.http.get<PaginatedResponse>(this.apiUrl,   
      { params, headers});;
  }

  getEmployee(id: number): Observable<Employee> {
    return this.http.get<Employee>(`${this.apiUrl}/${id}`);
  }

  createEmployee(employee: Employee): Observable<Employee> {
    return this.http.post<Employee>(this.apiUrl, employee);
  }

  updateEmployee(id: number, employee: Employee): Observable<Employee> {
    return this.http.put<Employee>(`${this.apiUrl}/${id}`, employee);
  }

  deleteEmployee(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  searchEmployees(pageNumber: number, pageSize: number, searchTerm: string): Observable<Employee[]> {
    const headers = new HttpHeaders({
      'X-API-KEY': this.apiKey
    });
    const params = new HttpParams()
    .set('pageNumber', pageNumber.toString())
    .set('pageSize', pageSize.toString())
    .set('searchTerm', searchTerm);

    return this.http.get<Employee[]>(`${this.apiUrl}/search`, { params, headers });
  }
}
