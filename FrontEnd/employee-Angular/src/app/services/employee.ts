import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Employee } from '../models/employee';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class Employeeservice {
  private apiUrl = 'https://localhost:5001/api/employees'; 

  constructor(private http: HttpClient){}

  getEmployees(): Observable<Employee[]> {
  return this.http.get<Employee[]>(this.apiUrl);
}

getEmployeeById(id: number): Observable<Employee> {
  return this.http.get<Employee>(`${this.apiUrl}/${id}`);
}

createEmployee(employee: Employee): Observable<Employee> {
  return this.http.post<Employee>(this.apiUrl, employee);
}

updateEmployee(employee: Employee): Observable<Employee> {
  return this.http.put<Employee>(`${this.apiUrl}/${employee['Id']}`, employee);
}

deleteEmployee(id: number): Observable<any> {
  return this.http.delete(`${this.apiUrl}/${id}`);
}
}

export type { Employee };
