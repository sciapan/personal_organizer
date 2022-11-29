import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UiService {
  private showBirthday: boolean = false;
  private subject = new Subject<any>();

  constructor() { }

  toggleAddBirthday(): void {
    this.showBirthday = !this.showBirthday;
    this.subject.next(this.showBirthday);
  }

  onToggle(): Observable<any> {
    return this.subject.asObservable();
  }
}
