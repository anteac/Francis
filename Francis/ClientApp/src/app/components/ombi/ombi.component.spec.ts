import { async, ComponentFixture, TestBed } from '@angular/core/testing';

describe('OmbiComponent', () => {
  let component: OmbiComponent;
  let fixture: ComponentFixture<OmbiComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OmbiComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OmbiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
