import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from './user';

@Injectable({ providedIn: 'root' })
export class AccountService {
    private userSubject: BehaviorSubject<User>;
    public user: Observable<User>;

    constructor(
        private router: Router,
        private http: HttpClient
    ) {
        this.userSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('user')));
        this.user = this.userSubject.asObservable();
    }

    public get userValue(): User {
        return this.userSubject.value;
    }

    login(username, password) {

        //#temp
        return this.http.post<any>(`https://dev.sitemercado.com.br/api/login`, { username, password },
        { headers : { "Authorization": "Basic " + btoa(username + ":" + password) } })
            .pipe(map(result => {
                
                if (result.success) { 
                    localStorage.setItem('user token', JSON.stringify(result));
                    this.router.navigate(['/'])
                }else {
                    alert("Usuário ou senha inválidos")
                }
            }));
    }

    logout() {
        // remove user from local storage and set current user to null
        localStorage.removeItem('user');
        this.userSubject.next(null);
        this.router.navigate(['/account/login']);
    }
}