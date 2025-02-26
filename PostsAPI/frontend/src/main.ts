import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.routes';
import { AppComponent } from './app/app.component';
import { provideHttpClient } from '@angular/common/http';

bootstrapApplication(AppComponent, {providers: [...appConfig,provideHttpClient()]}).catch((err) => console.error(err));
