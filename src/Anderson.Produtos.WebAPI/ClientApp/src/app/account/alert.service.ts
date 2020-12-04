import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { filter } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class AlertService {
    private subject = new Subject<string>();
    private defaultId = 'default-alert';

    // enable subscribing to alerts observable
    // onAlert(id = this.defaultId): Observable<string> {
    //     return this.subject.asObservable().pipe());
    // }

    // convenience methods
    success(message: string, options?: any) {
        alert(message);
    }

    error(message: string, options?: any) {
        alert(message);
    }

    info(message: string, options?: any) {

        alert(message);
    }

    warn(message: string, options?: any) {
        alert(message);
    }

    // main alert method    
    alert(alert: any) {
        alert.id = alert.id || this.defaultId;
        this.subject.next(alert);
    }

    // clear alerts
    clear(id = this.defaultId) {
        // this.subject.next(new Alert({ id }));
    }
}