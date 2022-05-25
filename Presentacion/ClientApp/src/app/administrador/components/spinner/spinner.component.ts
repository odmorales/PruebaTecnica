import { Component, Input } from '@angular/core';
import { MediaObserver } from '@angular/flex-layout';

@Component({
  selector: 'app-spinner',
  templateUrl: './spinner.component.html',
  styles: [
  ]
})
export class SpinnerComponent {

  @Input() lista!: any[];

  constructor(public media: MediaObserver) {}

  get getImage(): string {
    return this.media.isActive('xs') ? 'assets/img_notFound_xs.svg': 'assets/img_notFound.svg';
  }

}
