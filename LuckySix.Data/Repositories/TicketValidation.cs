﻿using LuckySix.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LuckySix.Data.Repositories
{
  public class TicketValidation : ITicketValidation
  {
    private readonly IUserRepository userRepository;
    public TicketValidation()
    {
      userRepository = new UserRepository();
    }

    public async Task<bool> IsPossibleBetting(decimal stake, int userId)
    {
      var user = await userRepository.GetUser(userId);
      decimal balance = user.Balance;
      if (stake > balance)
      {
        return false;
      }

      return true;
    }

    public bool IsValidSelectedNumbers(string numbers)
    {
   
      if (!IsStringInFormat(numbers)) return false;
      if (numbers.Length < 11) return false;

      var selectedNum = numbers.Split(',').Select(Int32.Parse).ToList();
      if (selectedNum.Count != 6) return false;
      for (int i = 0; i < selectedNum.Count - 1; i += 1)
      {
        if (selectedNum[i] < 1 || selectedNum[i] > 48) return false;
        for (int j = i + 1; j < selectedNum.Count; j += 1)
        {
          if (selectedNum[i] == selectedNum[j]) return false;
        }
      }

      return true;
    }

    public bool IsStringInFormat(string str)
    {
      Regex rgx = new Regex(@"\D");
      int counter = 0;
      foreach(Match match in rgx.Matches(str))
      {
        counter++;
      }
      if(counter != 5)
      {
        return false;
      }

        return true;
    }

    public bool IsValidStake(decimal stake)
    {
      if (stake < 1 || stake > 100 || stake.Equals(null)) return false;

      return true;

    }
  }
}
