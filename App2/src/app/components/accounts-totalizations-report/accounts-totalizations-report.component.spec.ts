import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountsTotalizationsReportComponent } from './accounts-totalizations-report.component';

describe('AccountsTotalizationsReportComponent', () => {
  let component: AccountsTotalizationsReportComponent;
  let fixture: ComponentFixture<AccountsTotalizationsReportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AccountsTotalizationsReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccountsTotalizationsReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
