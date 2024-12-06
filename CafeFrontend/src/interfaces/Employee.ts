export interface Employee {
  id: string;
  name: string;
  emailAddress: string;
  phoneNumber: string;
  gender: string;
  daysWorked: number;
  cafe?: string;
  startDate?: string | null;
}
