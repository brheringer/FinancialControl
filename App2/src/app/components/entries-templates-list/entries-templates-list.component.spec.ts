import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EntriesTemplatesListComponent } from './entries-templates-list.component';

describe('EntriesTemplatesListComponent', () => {
  let component: EntriesTemplatesListComponent;
  let fixture: ComponentFixture<EntriesTemplatesListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EntriesTemplatesListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EntriesTemplatesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
