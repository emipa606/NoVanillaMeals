# Copilot Instructions for "No Vanilla Meals" Mod Development

## Mod Overview and Purpose
The "No Vanilla Meals" mod is designed to enhance the RimWorld gameplay experience by removing all vanilla meal definitions from the game. This mod is specifically for players who wish to engage exclusively with modded meals, providing a fresh and unique culinary experience within the game. Importantly, the mod preserves base classes so that other mods can still use them, ensuring broad compatibility and flexibility for further modifications.

## Key Features and Systems
- **Removal of Vanilla Meals**: The core feature of the mod is the complete removal of all vanilla meal definitions, allowing only modded meals to appear in the game.
- **Compatibility with Other Mods**: Base classes remain untouched, ensuring that the mod does not interfere with other mods that rely on these base classes.
- **Simple and Focused Implementation**: The mod consists of a straightforward XML patch and a C# script to efficiently manage meal definitions.

## Coding Patterns and Conventions
- The mod follows standard C# conventions, adhering to clear and consistent naming conventions for classes and methods.
- The project's structure is organized into a single C# file and a corresponding XML file to facilitate ease of management and clarity.
- Member visibility and encapsulation are used appropriately to maintain clean and maintainable code.

## XML Integration
- The mod leverages XML for defining and removing meal-related content.
- The XML file (`Items_Food_Patch.xml`) contains `RecipeDef` and `ThingDef` definitions that are employed to redefine and manipulate meal-related data within RimWorld.

## Harmony Patching
- While the current mod functionality does not require extensive Harmony patches, it lays the groundwork for such implementations if future expansions necessitate more intricate alterations.
- Harmony can be employed for method interception, should the mod evolve to require deeper integration with existing game mechanics or systems.

## Suggestions for Copilot
To maximize the utility of GitHub Copilot in this project, consider the following suggestions:
- Use Copilot to assist with refactoring and optimizing existing code, ensuring adherence to best practices.
- Employ Copilot to explore additional XML manipulations, such as advanced patching techniques, that could further streamline meal management.
- Aid in the creation of potential expansions or spin-off mods by generating base templates and functionality lookups in C# and XML.
- Leverage Copilot's capabilities to produce necessary documentation or boilerplate comments that improve code readability and maintainability.

By following these instructions and suggestions, developers can maintain the "No Vanilla Meals" mod effectively while harnessing Copilot's strengths to streamline development workflows and explore new modding possibilities in RimWorld.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

