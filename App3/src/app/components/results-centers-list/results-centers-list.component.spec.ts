import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ResultsCentersListComponent } from './results-centers-list.component';

describe('ResultsCentersListComponent', () => {
  let component: ResultsCentersListComponent;
  let fixture: ComponentFixture<ResultsCentersListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ResultsCentersListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ResultsCentersListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
