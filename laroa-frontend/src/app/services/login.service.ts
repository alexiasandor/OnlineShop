import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class LoginService {
private url = 'api/admin';
private path = 'https://localhost:7066/'

  constructor(private http: HttpClient) {

  }
  public login(username: string | null, password: string | null){
    //connect to backend
    //da aici am conectat tabelul de produse deci trb modificat
    //TODO
    console.log('click',username,password);
    this.http.get(this.path+this.url).subscribe(request=> {console.log('request', request)});
  }

}


