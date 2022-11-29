import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Birthday } from 'src/app/Birthday';
import { faTimes } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-birthday-item',
  templateUrl: './birthday-item.component.html',
  styleUrls: ['./birthday-item.component.css']
})
export class BirthdayItemComponent implements OnInit {
  @Input() birthday!: Birthday;
  faTimes = faTimes;
  @Output() onDeleteBirthday: EventEmitter<Birthday> = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }

  onDelete(birthday: Birthday) {
    this.onDeleteBirthday.emit(birthday);
  }
}
