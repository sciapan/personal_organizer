import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BirthdayItemComponent } from './birthday-item.component';

describe('BirthdayItemComponent', () => {
  let component: BirthdayItemComponent;
  let fixture: ComponentFixture<BirthdayItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BirthdayItemComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BirthdayItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
