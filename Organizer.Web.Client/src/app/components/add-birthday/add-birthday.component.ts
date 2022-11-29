import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Subscription } from 'rxjs';
import { Birthday } from 'src/app/Birthday';
import { UiService } from 'src/app/services/ui.service';

@Component({
  selector: 'app-add-birthday',
  templateUrl: './add-birthday.component.html',
  styleUrls: ['./add-birthday.component.css']
})
export class AddBirthdayComponent implements OnInit {
  @Output() onAddBirthday: EventEmitter<Birthday> = new EventEmitter();
  person!: string;
  dob!: Date;
  notes: string | undefined;
  showAddBirthday: boolean = false;
  subscription: Subscription;

  constructor(private uiService: UiService) {
    this.subscription = this.uiService.onToggle().subscribe(value => this.showAddBirthday = value)
  }

  ngOnInit(): void {
  }

  onSubmit() {
    this.onAddBirthday.emit({ person: this.person, dob: this.dob, notes: this.notes });

    this.person = '';
    this.dob = new Date();
    this.notes = '';
  }
}
