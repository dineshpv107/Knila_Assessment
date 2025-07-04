import { Pipe, PipeTransform } from '@angular/core';
import i18next from 'i18next';

@Pipe({
  name: 'translate',
  pure: false
})
export class TranslatePipe implements PipeTransform {

  transform(value: string, options?: any): string {
    return i18next.t(value, options);
  }

}
