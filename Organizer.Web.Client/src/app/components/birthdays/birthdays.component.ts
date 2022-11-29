import { Component, OnInit } from '@angular/core';
import { Birthday } from 'src/app/Birthday';
import { BirthdaysService } from 'src/app/services/birthdays.service';
import { HttpClient, HttpHeaders } from '@angular/common/http'

@Component({
  selector: 'app-birthdays',
  templateUrl: './birthdays.component.html',
  styleUrls: ['./birthdays.component.css']
})
export class BirthdaysComponent implements OnInit {
  birthdays: Birthday[] = [];

  constructor(private birthdaysService: BirthdaysService) { }

  ngOnInit(): void {
    this.birthdaysService.getBirthdays().subscribe((birthdays) => this.birthdays = birthdays);
  }

  deleteBirthday(birthday: Birthday) {
    this.birthdaysService
      .deleteBirthday(birthday)
      .subscribe((birthday) => this.birthdays = this.birthdays.filter((x) => x.id !== birthday.id));
  }

  addBirthday(birthday: Birthday) {
    this.birthdaysService.addBirthday(birthday).subscribe((birthday) => (this.birthdays.push(birthday)));
  }
}
