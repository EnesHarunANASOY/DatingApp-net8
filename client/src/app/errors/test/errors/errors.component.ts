import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-errors',
  standalone: true,
  imports: [],
  templateUrl: './errors.component.html',
  styleUrl: './errors.component.css'
})
export class ErrorsComponent {
 baseUrl =environment.apiUrl;
 private http = inject(HttpClient);
 validationErrors : string[] = [];

 get400Error(){
  this.http.get(this.baseUrl+'buggy/bad-request').subscribe({
    next: response => console.log(response),
    error: error=> console.log(error) 
  })
 }

 get401Error(){
  this.http.get(this.baseUrl+'buggy/auth').subscribe({
    next: response => console.log(response),
    error: error=> console.log(error) 
  })
 }

 get404Error(){
  this.http.get(this.baseUrl+'buggy/not-found').subscribe({
    next: response => console.log(response),
    error: error=> console.log(error) 
  })
 }

 get500Error(){
  this.http.get(this.baseUrl+'buggy/server-error').subscribe({
    next: response => console.log(response),
    error: error=> console.log(error) 
  })
 }

 get400ValidationError(){
  this.http.get(this.baseUrl+'account/register',{}).subscribe({
    next: response => {
      console.log(response)

    },
    error: error=> {
      console.log(error) 
      this.validationErrors = error;
      console.log("ERROR is",error)
      console.log("Length is",this.validationErrors.length)

    }
  })
 }
}
