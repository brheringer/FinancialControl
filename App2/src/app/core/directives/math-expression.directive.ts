import { Directive, HostListener, ElementRef, OnInit } from "@angular/core";
import { NgControl } from "@angular/forms";

@Directive({
  selector: "[finMathExpression]"
})
export class MathExpressionDirective implements OnInit {
  private el: any;

  constructor(public ngControl: NgControl, private elementRef: ElementRef) {
    this.el = this.elementRef.nativeElement;
  }

  ngOnInit() {
    this.el.value = this.resolveExpression();
  }

  @HostListener("blur", [ "$event"])
  onNgModelChange(event) {
    let newVal = this.resolveExpression();
    // this.ngControl.valueAccessor.writeValue(newVal);
    this.el.value = newVal;
    this.el.dispatchEvent(new Event('input'))
  }

  resolveExpression(): number {
    let value = this.ngControl.control.value;

    if(value && value.length > 1 && value[0] == '=') {
      const END_MARK = '¢';

      let text = value.substring(1).replace(/\s/g, "") + END_MARK;
      //TODO separador de decimal
      let a: number = null;
      let b: number = null;
      let buffer = '';
      let op = "";
      for(let i = 0; i < text.length; i++) {
        let c = text[i];
        //TODO sinal antes do numero
        if(c == "+" || c == "-" || c == "*" || c == "/" || c == END_MARK) {
          if(a == null) {
            a = Number(buffer);
          }
          else {
            b = Number(buffer);
            a = this.operate(a, op, b);
            b = null;
          }
          buffer = '';
          op = c;
        }
        else {
          //TODO conferir se c é número?
          buffer = buffer + c;
        }
      }
      return a;
    }

    else return isNaN(Number(value)) ? 0 : Number(value);
  }

  operate(a: number, op: string, b: number): number {
    return op == "+"
      ? a + b
      : op == "-"
      ? a - b
      : op == "*"
      ? this.round(a * b, 2) //TODO deveria parametrizar a precisao
      : op == "/"
      ? this.round(a / b, 2) //TODO deveria parametrizar a precisao
      : op == "" //sem operacao, por exemplo, se entrar so um numero
      ? a
      : NaN;
  }

  round(value: number, precision: number): number {
    let multiplier = Math.pow(10, precision || 0);
    return Math.round(value * multiplier) / multiplier;
  }
}
