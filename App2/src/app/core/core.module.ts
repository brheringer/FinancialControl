import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgbTypeaheadModule } from '@ng-bootstrap/ng-bootstrap';

import { MyHttpInterceptor } from './services/my-http-interceptor';
import { GenericService } from './services/generic.service';
import { LoginService } from './services/login.service';

import { AlertComponent } from './components/alert/alert.component';
import { LoginComponent } from './components/login/login.component';
import { BooleanFieldComponent } from './components/boolean-field/boolean-field.component';
import { DateFieldComponent } from './components/date-field/date-field.component';
import { NumberFieldComponent } from './components/number-field/number-field.component';
import { SelectEntityFieldComponent } from './components/select-entity-field/select-entity-field.component';
import { SelectPrimitiveFieldComponent } from './components/select-primitive-field/select-primitive-field.component';
import { SmartSearchFieldComponent } from './components/smart-search-field/smart-search-field.component';
import { RichLabelComponent } from './components/rich-label/rich-label.component';
import { TextAreaFieldComponent } from './components/text-area-field/text-area-field.component';
import { TextFieldComponent } from './components/text-field/text-field.component';
import { DateFormatPipe } from './pipes/date-pipe';
import { EntityFormatPipe } from './pipes/entity-pipe';
import { MathExpressionDirective } from './directives/math-expression.directive';

@NgModule({
  declarations: [
    AlertComponent,
    LoginComponent,
    BooleanFieldComponent,
    DateFieldComponent,
    NumberFieldComponent,
    RichLabelComponent,
    SelectEntityFieldComponent,
    SelectPrimitiveFieldComponent,
    SmartSearchFieldComponent,
    TextAreaFieldComponent,
    TextFieldComponent,
    DateFormatPipe,
    EntityFormatPipe,
    MathExpressionDirective
  ],
  imports: [
    BrowserModule,
    CommonModule,
    FormsModule,
    HttpClientModule,
    NgbTypeaheadModule
  ],
  exports: [
    AlertComponent,
    LoginComponent,
    BooleanFieldComponent,
    DateFieldComponent,
    NumberFieldComponent,
    RichLabelComponent,
    SelectEntityFieldComponent,
    SelectPrimitiveFieldComponent,
    SmartSearchFieldComponent,
    TextAreaFieldComponent,
    TextFieldComponent,
    DateFormatPipe,
    EntityFormatPipe,
    MathExpressionDirective
  ],
  providers: [
    GenericService,
    LoginService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: MyHttpInterceptor,
      multi: true,
    }
  ]
})
export class FinancialCoreModule { }
