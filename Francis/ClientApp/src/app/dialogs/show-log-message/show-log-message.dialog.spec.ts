import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ShowLogMessageDialog } from './show-log-message.dialog';

describe('LogsComponent', () => {
  let component: ShowLogMessageDialog;
  let fixture: ComponentFixture<ShowLogMessageDialog>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ShowLogMessageDialog]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowLogMessageDialog);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
