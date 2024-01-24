# Sprout.Exam.WebApp

# Changes made
1. Modified the structure of the initial state.
2. Added and configured new DbContext for employee.
3. Added AutoMapper.
4. Added Employee service to hande employee related functionalities.
5. Added Income Service to handle income related functionalities.
6. Added Repository under DataAccess.
7. Added Employee entity to represent the Employee table.
8. Include Salary settings in appsetting.json.
9. Implemented Abstraction for different employee type
10. Added Service Registry to handle dependency injection, Registration of Context and Salary Settings configuration.


# Suggestions
1. Update the nuget dependencies with the latest available.
   - Updates often include patches for known vulnerabilities, enhancing the overall security of the application.
   - And we can also avoid issues related to using obsolete or deprecated technologies.
2. Separate the identity server solution, Web API and the Client App (UI).
   - I'm always a fan of microservices architecture as each component can scale independently, allowing the developers to allocate resources where needed based on the specific demands of each part of your system.
3. Consider Bonuses, Allowances (Taxable and non taxable), and other mandatory deductions
  - It will also be base on employee type.
  - We can consider including them as part of computation of net income.
