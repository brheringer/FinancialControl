import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MemosMappingsListComponent } from './memos-mappings-list.component';

describe('MemosMappingsListComponent', () => {
  let component: MemosMappingsListComponent;
  let fixture: ComponentFixture<MemosMappingsListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MemosMappingsListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MemosMappingsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
