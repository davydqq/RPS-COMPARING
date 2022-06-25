import { Module } from '@nestjs/common';
import { DbModule } from '../db/db.module';
import { FilmsController } from './controllers/films.controller';
import { FilmsService } from './services/films/films.service';

@Module({
  imports: [DbModule],
  controllers: [FilmsController],
  providers: [FilmsService],
})
export class FilmsModule {}
